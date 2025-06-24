using NJsonSchema;
using OpenWebUISharp.Models;
using OpenWebUISharp.Models.ChatCompletions;
using OpenWebUISharp.Models.ChatCompletions.API;
using OpenWebUISharp.Models.Knowledgebases;
using OpenWebUISharp.Models.Knowledgebases.API;
using OpenWebUISharp.Models.Models;
using OpenWebUISharp.Models.Models.API;
using OpenWebUISharp.Models.Tools;
using SerializableHttps;
using SerializableHttps.AuthenticationMethods;
using System.Text.Json;

namespace OpenWebUISharp
{
	/// <summary>
	/// Implementation of the wrapper
	/// </summary>
	public class OpenWebUIWrapper : IOpenWebUIWrapper
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
		public OpenWebUIWrapper(string token, string apiUrl)
		{
			Token = token;
			APIURL = apiUrl;

			_client = new SerializableHttpsClient();
			_client.SetAuthentication(new JWTAuthenticationMethod(token));
		}

		#region Models

		/// <summary>
		/// Get all models
		/// </summary>
		/// <returns></returns>
		public async Task<List<Model>> GetAllModels()
		{
			var result = await _client.GetAsync<GetAllModelsResponse>(APIURL + "/api/models");
			return result.Data;
		}

		#endregion

		#region ChatCompletion

		/// <summary>
		/// Query a given model with a conversation.
		/// </summary>
		/// <param name="conversation">A list of messages and roles that have occured</param>
		/// <param name="modelId">The ID of the model you want to use</param>
		/// <param name="options">Optional set of options you can use.</param>
		/// <returns></returns>
		public async Task<ConversationMessage> Query(Conversation conversation, string modelId, ConversationOptions? options = null)
		{
			if (options == null)
				options = new ConversationOptions();
			var request = new ChatCompletionRequest()
			{
				Model = modelId,
				Messages = conversation.Messages.Select(x => new ChatCompletionMessage() { Content = x.Message, Role = x.Role }).ToList(),
				Files = options.KnowledgebaseIDs.Select(x => new ChatCompletionFile() { ID = x, Legacy = true }).ToList(),
				ToolIDs = options.ToolIDs,
				Parameters = new ChatCompletionParameters()
				{
					Temperature = options.Temperature
				}
			};
			var response = await _client.PostAsync<ChatCompletionRequest, ChatCompletionResponse>(request, APIURL + "/api/chat/completions");
			if (response.Choices.Count == 0)
				throw new Exception("Invalid response from OpenWebUI!");
			var newMessage = new ConversationMessage(
				response.Choices[0].Message.Role,
				response.Choices[0].Message.Content);
			if (response.Sources != null && response.Sources.Count > 0 && response.Sources[0].MetaData != null)
				newMessage.RAGFiles = response.Sources[0].MetaData!.Select(x => new ConversationMessageRAGFile(x.ID, x.Name, x.Score)).ToList();
			if (options.RemoveThinking)
				newMessage.Message = RemoveThinking(newMessage.Message);
			return newMessage;
		}

		/// <summary>
		/// Query a given model, and force it to output the response in a JSON object that can be deserialized into <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">Some non-nullable json serialisable object</typeparam>
		/// <param name="conversation">A list of messages and roles that have occured</param>
		/// <param name="modelId">The ID of the model you want to use</param>
		/// <param name="options">Optional set of options you can use.</param>
		/// <returns></returns>
		public async Task<T> QueryToObject<T>(Conversation conversation, string modelId, ConversationOptions? options = null) where T : notnull
		{
			if (options == null)
				options = new ConversationOptions();
			var request = new ChatCompletionRequest()
			{
				Model = modelId,
				Messages = conversation.Messages.Select(x => new ChatCompletionMessage() { Content = x.Message, Role = x.Role }).ToList(),
				Files = options.KnowledgebaseIDs.Select(x => new ChatCompletionFile() { ID = x, Legacy = true }).ToList(),
				ToolIDs = options.ToolIDs,
				Parameters = new ChatCompletionParameters()
				{
					Temperature = options.Temperature
				},
				Format = JsonSerializer.Deserialize<object>(JsonSchema.FromType<T>().ToJson())
			};
			var response = await _client.PostAsync<ChatCompletionRequest, ChatCompletionResponse>(request, APIURL + "/api/chat/completions");
			if (response.Choices.Count == 0)
				throw new Exception("Invalid response from OpenWebUI!");
			var deserialized = JsonSerializer.Deserialize<T>(response.Choices[0].Message.Content);
			if (deserialized == null)
				throw new Exception("OpenWebUI did not respond with a valid JSON response!");
			return deserialized;
		}

		private string RemoveThinking(string text)
		{
			var thinkStart = text.IndexOf("<think>");
			var thinkEnd = text.IndexOf("</think>");

			if (thinkStart != -1 && thinkEnd != -1 && thinkEnd > thinkStart)
				text = text.Remove(thinkStart, thinkEnd - thinkStart + 8);
			return text;
		}

		#endregion

		#region Knowledge base

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
		public async Task<KnowledgebaseModel> GetKnowledgebase(string id)
		{
			var response = await _client.GetAsync<OpenWebUIKnowledgebaseModel>(APIURL + "/api/v1/knowledge/" + id);
			return Convert(response);
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
		public async Task DeleteKnowledgebase(string id)
		{
			var knowledgebase = await GetKnowledgebase(id);
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
		public async Task AddFileToKnowledgebase(string text, string knowledgebaseId, string fileName)
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
		public async Task RemoveFileFromCollection(string fileId, string knowledgebaseId)
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
				item.Files.Select(x => new KnowledgebaseFile(x.ID, x.MetaData.Name)).ToList(),
				DateTimeOffset.FromUnixTimeSeconds(item.CreatedAt).UtcDateTime,
				DateTimeOffset.FromUnixTimeSeconds(item.UpdatedAt).UtcDateTime);

		#endregion

		#region Tools

		/// <summary>
		/// Gets all tools
		/// </summary>
		/// <returns></returns>
		public async Task<List<ToolModel>> GetAllTools() => await _client.GetAsync<List<ToolModel>>(APIURL + "/api/v1/tools/");

		#endregion
	}
}
