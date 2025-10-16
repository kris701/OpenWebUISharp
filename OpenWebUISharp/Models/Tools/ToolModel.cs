using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Tools
{
	/// <summary>
	/// A tool in OpenWebUI
	/// </summary>
	public class ToolModel
	{
		/// <summary>
		/// ID of the tool
		/// </summary>
		[JsonPropertyName("id")]
		public string ID { get; set; }
		/// <summary>
		/// Name of the tool
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }
		/// <summary>
		/// Content of the tool
		/// </summary>
		[JsonPropertyName("content")]
		public string Content { get; set; }
		/// <summary>
		/// Description of the tool
		/// </summary>
		[JsonPropertyName("description")]
		public string Description { get; set; }
	}
}
