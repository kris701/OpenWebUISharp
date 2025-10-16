using OpenWebUISharp.Models.Tools;
using OpenWebUISharp.Models.Tools.API;
using SerializableHttps;
using SerializableHttps.AuthenticationMethods;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace OpenWebUISharp
{
	/// <summary>
	/// Implementation of the wrapper
	/// </summary>
	public class ToolsWrapper : IToolWrapper
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
		public ToolsWrapper(string token, string apiUrl)
		{
			Token = token;
			APIURL = apiUrl;


			_client = new SerializableHttpsClient();
			_client.SetAuthentication(new JWTAuthenticationMethod(token));
		}

		/// <summary>
		/// Gets all tools
		/// </summary>
		/// <returns></returns>
		public async Task<List<ToolModel>> GetAllTools()
		{
			var models = await _client.GetAsync<List<OpenWebUIToolModel>>(APIURL + "/api/v1/tools/list");
			var results = new List<ToolModel>();
			foreach (var model in models)
				results.Add(new ToolModel()
				{
					ID = model.ID,
					Name = model.Name,
					Content = model.Content,
					Description = model.Meta.Description
				});

			return results;
		}

		/// <summary>
		/// Add a tool
		/// </summary>
		/// <returns></returns>
		public async Task<ToolModel> AddTool(string name, string description, string content)
		{
			var model = await _client.PostAsync<OpenWebUIToolModel, OpenWebUIToolModel>(
				new OpenWebUIToolModel()
				{
					ID = name,
					Name = name,
					Content = content,
					Meta = new OpenWebUIToolMetaModel()
					{
						Description = description
					}
				},
				APIURL + "/api/v1/tools/create");
			return new ToolModel()
			{
				ID = model.ID,
				Name = model.Name,
				Content = content,
				Description = model.Meta.Description
			};
		}

		/// <summary>
		/// Add a tool by import url
		/// </summary>
		/// <returns></returns>
		public async Task<ToolModel> AddTool(string importUrl)
		{
			var client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
			var importResult = await client.SendAsync(new HttpRequestMessage()
			{
				Method = HttpMethod.Delete,
				RequestUri = new Uri(APIURL + "/ollama/api/delete"),
				Content = new StringContent(
					JsonSerializer.Serialize(new ImportToolModel() { URL = importUrl }),
					Encoding.UTF8,
					MediaTypeNames.Application.Json)
			});
			var str = await importResult.Content.ReadAsStringAsync();
			var addModel = JsonSerializer.Deserialize<OpenWebUIToolModel>(str);
			if (addModel == null)
				throw new Exception("Error during tool import!");

			return await AddTool(addModel.Name, addModel.Meta.Description, addModel.Content);
		}

		/// <summary>
		/// Deletes a tool by its ID
		/// </summary>
		/// <returns></returns>
		public async Task DeleteTool(string id)
		{
			await _client.DeleteAsync(
				APIURL + "/api/v1/tools/id/" + id + "/delete");
		}
	}
}
