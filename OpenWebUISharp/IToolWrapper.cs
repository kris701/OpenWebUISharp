using OpenWebUISharp.Models.Tools;

namespace OpenWebUISharp
{
	/// <summary>
	/// Interface for the OpenWebUI tools API
	/// </summary>
	public interface IToolWrapper
	{
		/// <summary>
		/// Gets all tools
		/// </summary>
		/// <returns></returns>
		public Task<List<ToolModel>> GetAll();
		/// <summary>
		/// Add a tool
		/// </summary>
		/// <returns></returns>
		public Task<ToolModel> Add(string name, string description, string content);
		/// <summary>
		/// Add a tool by import url
		/// </summary>
		/// <returns></returns>
		public Task<ToolModel> Add(string importUrl);
		/// <summary>
		/// Deletes a tool by its ID
		/// </summary>
		/// <returns></returns>
		public Task Delete(string id);
	}
}
