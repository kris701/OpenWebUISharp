using NJsonSchema;
using OpenWebUISharp.Models;
using OpenWebUISharp.Models.Knowledgebases;
using OpenWebUISharp.Models.Knowledgebases.API;
using SerializableHttps;
using SerializableHttps.AuthenticationMethods;
using System.Text.Json;

namespace OpenWebUISharp
{
	/// <summary>
	/// Implementation of the wrapper
	/// </summary>
	public class KnowledgebaseWrapper : IKnowledgebaseWrapper
	{
		/// <summary>
		/// The JWT token you can find in the OpenWebUI settings
		/// </summary>
		public string Token { get; set; }
		/// <summary>
		/// The URL (or IP) to OpenWebUI
		/// </summary>
		public string APIURL { get; set; }

		private readonly SerializableHttpsClient _client;

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="token"></param>
		/// <param name="apiUrl"></param>
		public KnowledgebaseWrapper(string token, string apiUrl)
		{
			Token = token;
			APIURL = apiUrl;

			_client = new SerializableHttpsClient();
			_client.SetAuthentication(new JWTAuthenticationMethod(token));
		}

		/// <summary>
		/// Get all knowledgebase collections
		/// </summary>
		/// <returns></returns>
		public async Task<List<KnowledgebaseModel>> GetAllKnowledgebases()
		{
			var response = await _client.GetAsync<List<OpenWebUIKnowledgebaseModel>>(APIURL + "/api/v1/knowledge/");
			var returnList = new List<KnowledgebaseModel>();
			foreach (var item in response)
				returnList.Add(Convert(item));
			return returnList;
		}

		/// <summary>
		/// Get a specific knowledgebase collection
		/// </summary>
		/// <param name="id">The unique ID of the knowledgebase</param>
		/// <returns></returns>
		public async Task<KnowledgebaseModel> GetKnowledgebaseByID(Guid id)
		{
			var response = await _client.GetAsync<OpenWebUIKnowledgebaseModel>(APIURL + "/api/v1/knowledge/" + id);
			return Convert(response);
		}

		/// <summary>
		/// Get a specific knowledgebase collection
		/// </summary>
		/// <param name="name">The unique ID of the knowledgebase</param>
		/// <returns></returns>
		public async Task<KnowledgebaseModel> GetKnowledgebaseByName(string name)
		{
			var all = await GetAllKnowledgebases();
			var target = all.FirstOrDefault(x => x.Name == name);
			if (target != null)
				return await GetKnowledgebaseByID(target.ID);
			throw new Exception($"No knowledgebase with the name '{name}' was found!");
		}

		/// <summary>
		/// Create a new knowledgebase collection with a given name
		/// </summary>
		/// <param name="name">The name of the new knowledgebase</param>
		/// <returns></returns>
		public async Task<KnowledgebaseModel> CreateKnowledgebase(string name)
		{
			var response = await _client.PostAsync<CreateKnowledgebaseInput, OpenWebUIKnowledgebaseModel>(
				new CreateKnowledgebaseInput()
				{
					Name = name,
					Description = "New Knowledgebase!"
				},
				APIURL + "/api/v1/knowledge/create");
			return Convert(response);
		}

		/// <summary>
		/// Deletes a knowledgebase and all the files in it.
		/// </summary>
		/// <param name="id">The unique ID of the knowledgebase</param>
		/// <returns></returns>
		public async Task DeleteKnowledgebase(Guid id)
		{
			var knowledgebase = await GetKnowledgebaseByID(id);
			foreach (var file in knowledgebase.Files)
				await RemoveFileFromCollection(file.ID, id);
			await _client.DeleteAsync(APIURL + "/api/v1/knowledge/" + knowledgebase.ID + "/delete");
		}

		/// <summary>
		/// Adds a new file to a knowledgebase.
		/// </summary>
		/// <param name="text">Content of the file you want to upload</param>
		/// <param name="knowledgebaseId">The unique ID of the knowledgebase</param>
		/// <param name="fileName">A filename to associate the file with</param>
		/// <returns></returns>
		public async Task AddFileToKnowledgebase(string text, Guid knowledgebaseId, string fileName)
		{
			var file = await UploadFile(text, fileName);
			await _client.PostAsync<CreateKnowledgebaseFileInput, EmptyModel>(
				new CreateKnowledgebaseFileInput()
				{
					FileID = file.ID,
				},
				APIURL + "/api/v1/knowledge/" + knowledgebaseId + "/file/add");
		}

		/// <summary>
		/// Removes a file from a knowledgebase
		/// </summary>
		/// <param name="fileId">The unique ID of the file</param>
		/// <param name="knowledgebaseId">The unique ID of the knowledgebase</param>
		/// <returns></returns>
		public async Task RemoveFileFromCollection(Guid fileId, Guid knowledgebaseId)
		{
			await _client.PostAsync<CreateKnowledgebaseFileInput, EmptyModel>(
				new CreateKnowledgebaseFileInput()
				{
					FileID = fileId,
				},
				APIURL + "/api/v1/knowledge/" + knowledgebaseId + "/file/remove");
		}

		private async Task<OpenWebUIFileModel> UploadFile(string text, string fileName)
		{
			var content = new MultipartFormDataContent();
			content.Add(new StreamContent(GenerateStreamFromString(text)), "file", fileName);
			var client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
			var fileResponse = await client.PostAsync(APIURL + "/api/v1/files/", content);
			var file = JsonSerializer.Deserialize<OpenWebUIFileModel>(await fileResponse.Content.ReadAsStringAsync());
			if (file == null)
				throw new Exception("OpenWebUI did not respond correctly!");

			// We have to wait until openwebui have "processed" the file!
			var fileStatus = new OpenWebUIFileStatus()
			{
				Data = new OpenWebUIFileStatusData()
				{
					Status = "?"
				}
			};
			while (fileStatus.Data.Status != "completed")
			{
				await Task.Delay(100);
				fileStatus = await _client.GetAsync<EmptyModel, OpenWebUIFileStatus>(
					new EmptyModel(),
					APIURL + "/api/v1/files/" + file.ID);
			}
			return file;
		}

		private static MemoryStream GenerateStreamFromString(string s)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}

		private KnowledgebaseModel Convert(OpenWebUIKnowledgebaseModel item) =>
			new KnowledgebaseModel(
				item.ID,
				item.Name,
				item.Files != null ? item.Files.Select(x => new KnowledgebaseFile(
					x.ID,
					x.MetaData.Name,
					DateTimeOffset.FromUnixTimeSeconds(x.CreatedAt).UtcDateTime,
					DateTimeOffset.FromUnixTimeSeconds(x.UpdatedAt).UtcDateTime)).ToList() : new List<KnowledgebaseFile>(),
				DateTimeOffset.FromUnixTimeSeconds(item.CreatedAt).UtcDateTime,
				DateTimeOffset.FromUnixTimeSeconds(item.UpdatedAt).UtcDateTime);
	}
}
