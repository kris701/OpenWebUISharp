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
		/// Main constructor
		/// </summary>
		/// <param name="iD"></param>
		/// <param name="name"></param>
		public ToolModel(string iD, string name)
		{
			ID = iD;
			Name = name;
		}
	}
}
