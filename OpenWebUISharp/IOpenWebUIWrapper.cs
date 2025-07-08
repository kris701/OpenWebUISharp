using OpenWebUISharp.Models.ChatCompletions;
using OpenWebUISharp.Models.Knowledgebases;
using OpenWebUISharp.Models.Models;
using OpenWebUISharp.Models.Tools;

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
		/// Get all models
		/// </summary>
		/// <returns></returns>
		public Task<List<Model>> GetAllModels();

		/// <summary>
		/// Query a given model with a conversation.
		/// </summary>
		/// <param name="conversation">A list of messages and roles that have occured</param>
		/// <param name="modelId">The ID of the model you want to use</param>
		/// <param name="options">Optional set of options you can use.</param>
		/// <returns></returns>
		public Task<ConversationMessage> Query(Conversation conversation, string modelId, ConversationOptions? options = null);
		/// <summary>
		/// Query a given model, and force it to output the response in a JSON object that can be deserialized into <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">Some non-nullable json serialisable object</typeparam>
		/// <param name="conversation">A list of messages and roles that have occured</param>
		/// <param name="modelId">The ID of the model you want to use</param>
		/// <param name="options">Optional set of options you can use.</param>
		/// <returns></returns>
		public Task<T> QueryToObject<T>(Conversation conversation, string modelId, ConversationOptions? options = null) where T : notnull;

		/// <summary>
		/// Get all knowledgebase collections
		/// </summary>
		/// <returns></returns>
		public Task<List<KnowledgebaseModel>> GetAllKnowledgebases();
		/// <summary>
		/// Get a specific knowledgebase collection
		/// </summary>
		/// <param name="id">The unique ID of the knowledgebase</param>
		/// <returns></returns>
		public Task<KnowledgebaseModel> GetKnowledgebaseByID(Guid id);
		/// <summary>
		/// Get a specific knowledgebase collection
		/// </summary>
		/// <param name="name">The unique name of the knowledgebase. If multiple knowledgebases have the same name, the first one is returned.</param>
		/// <returns></returns>
		public Task<KnowledgebaseModel> GetKnowledgebaseByName(string name);
		/// <summary>
		/// Create a new knowledgebase collection with a given name
		/// </summary>
		/// <param name="name">The name of the new knowledgebase</param>
		/// <returns></returns>
		public Task<KnowledgebaseModel> CreateKnowledgebase(string name);
		/// <summary>
		/// Deletes a knowledgebase and all the files in it.
		/// </summary>
		/// <param name="id">The unique ID of the knowledgebase</param>
		/// <returns></returns>
		public Task DeleteKnowledgebase(Guid id);
		/// <summary>
		/// Adds a new file to a knowledgebase.
		/// </summary>
		/// <param name="text">Content of the file you want to upload</param>
		/// <param name="knowledgebaseId">The unique ID of the knowledgebase</param>
		/// <param name="fileName">A filename to associate the file with</param>
		/// <returns></returns>
		public Task AddFileToKnowledgebase(string text, Guid knowledgebaseId, string fileName);
		/// <summary>
		/// Removes a file from a knowledgebase
		/// </summary>
		/// <param name="fileId">The unique ID of the file</param>
		/// <param name="knowledgebaseId">The unique ID of the knowledgebase</param>
		/// <returns></returns>
		public Task RemoveFileFromCollection(Guid fileId, Guid knowledgebaseId);

		/// <summary>
		/// Gets all tools
		/// </summary>
		/// <returns></returns>
		public Task<List<ToolModel>> GetAllTools();
	}
}
