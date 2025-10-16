using OpenWebUISharp.Models.Models;

namespace OpenWebUISharp
{
	/// <summary>
	/// Interface for the OpenWebUI models API
	/// </summary>
	public interface IModelsWrapper
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
		public Task<List<Model>> GetAll();
		/// <summary>
		/// Adds a model from ollama
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Task<Model> Pull(string name);
		/// <summary>
		/// Deletes an existing ollama model
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task DeleteByID(string id);
	}
}
