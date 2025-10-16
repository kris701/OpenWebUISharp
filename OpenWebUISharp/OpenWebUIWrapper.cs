using SerializableHttps;
using SerializableHttps.AuthenticationMethods;

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
		/// <summary>
		/// API for the knowledgebase
		/// </summary>
		public IKnowledgebaseWrapper Knowledgebase { get; }
		/// <summary>
		/// API for the models
		/// </summary>
		public IModelsWrapper Models { get; }
		/// <summary>
		/// API for querying
		/// </summary>
		public IQueryWrapper Query { get; }
		/// <summary>
		/// API for the tools
		/// </summary>
		public IToolWrapper Tools { get; }

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
			Knowledgebase = new KnowledgebaseWrapper(token, apiUrl);
			Models = new ModelsWrapper(token, apiUrl);
			Query = new QueryWrapper(token, apiUrl);
			Tools = new ToolsWrapper(token, apiUrl);

			_client = new SerializableHttpsClient();
			_client.SetAuthentication(new JWTAuthenticationMethod(token));
		}
	}
}
