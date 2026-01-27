namespace OpenWebUISharp
{
	/// <summary>
	/// Interface for the wrapper
	/// </summary>
	public interface IOpenWebUIWrapper
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
		/// <summary>
		/// API for users
		/// </summary>
		public IUsersWrapper Users { get; }
	}
}
