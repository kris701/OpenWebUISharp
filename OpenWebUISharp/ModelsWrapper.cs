using NJsonSchema;
using OpenWebUISharp.Models.Models;
using OpenWebUISharp.Models.Models.API;
using SerializableHttps;
using SerializableHttps.AuthenticationMethods;
using System.Net.Mime;
using System.Text;

namespace OpenWebUISharp
{
	/// <summary>
	/// Implementation of the wrapper
	/// </summary>
	public class ModelsWrapper : IModelsWrapper
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
		public ModelsWrapper(string token, string apiUrl)
		{
			Token = token;
			APIURL = apiUrl;

			_client = new SerializableHttpsClient();
			_client.SetAuthentication(new JWTAuthenticationMethod(token));
		}

		/// <summary>
		/// Get all models
		/// </summary>
		/// <returns></returns>
		public async Task<List<Model>> GetAllModels()
		{
			var result = await _client.GetAsync<GetAllModelsResponse>(APIURL + "/api/v1/models");
			return result.Data;
		}

		/// <summary>
		/// Adds a model from ollama
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public async Task<Model> PullModel(string name)
		{
			await _client.PostAsync<OpenWebUIAddModel, string>(
				new OpenWebUIAddModel()
				{
					Model = name
				},
				APIURL + "/ollama/api/pull");

			var allModels = await GetAllModels();
			var target = allModels.FirstOrDefault(x => x.ID == name);
			if (target == null)
				throw new Exception("Model not found!");
			return target;
		}

		/// <summary>
		/// Deletes an existing ollama model
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task DeleteModelByID(string id)
		{
			var client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
			await client.SendAsync(new HttpRequestMessage()
			{
				Method = HttpMethod.Delete,
				RequestUri = new Uri(APIURL + "/ollama/api/delete"),
				Content = new StringContent(
					$"{{\"model\":\"{id}\"}}",
					Encoding.UTF8,
					MediaTypeNames.Application.Json)
			});
		}
	}
}
