using OpenWebUISharp.Models.Knowledgebases;

namespace OpenWebUISharp
{
	/// <summary>
	/// Interface for the OpenWebUI knowledgebase API
	/// </summary>
	public interface IKnowledgebaseWrapper
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
		/// Get all knowledgebase collections
		/// </summary>
		/// <returns></returns>
		public Task<List<KnowledgebaseModel>> GetAll();
		/// <summary>
		/// Get a specific knowledgebase collection
		/// </summary>
		/// <param name="id">The unique ID of the knowledgebase</param>
		/// <returns></returns>
		public Task<KnowledgebaseModel> GetByID(Guid id);
		/// <summary>
		/// Get a specific knowledgebase collection
		/// </summary>
		/// <param name="name">The unique name of the knowledgebase. If multiple knowledgebases have the same name, the first one is returned.</param>
		/// <returns></returns>
		public Task<KnowledgebaseModel> GetByName(string name);
		/// <summary>
		/// Create a new knowledgebase collection with a given name
		/// </summary>
		/// <param name="name">The name of the new knowledgebase</param>
		/// <returns></returns>
		public Task<KnowledgebaseModel> Add(string name);
		/// <summary>
		/// Deletes a knowledgebase and all the files in it.
		/// </summary>
		/// <param name="id">The unique ID of the knowledgebase</param>
		/// <returns></returns>
		public Task Delete(Guid id);
		/// <summary>
		/// Adds a new file to a knowledgebase.
		/// </summary>
		/// <param name="text">Content of the file you want to upload</param>
		/// <param name="knowledgebaseId">The unique ID of the knowledgebase</param>
		/// <param name="fileName">A filename to associate the file with</param>
		/// <returns></returns>
		public Task AddFile(string text, Guid knowledgebaseId, string fileName);
		/// <summary>
		/// Removes a file from a knowledgebase
		/// </summary>
		/// <param name="fileId">The unique ID of the file</param>
		/// <param name="knowledgebaseId">The unique ID of the knowledgebase</param>
		/// <returns></returns>
		public Task DeleteFile(Guid fileId, Guid knowledgebaseId);
	}
}
