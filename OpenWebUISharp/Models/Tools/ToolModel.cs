using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Tools
{
	public class ToolModel
	{
		[JsonPropertyName("id")]
		public string ID { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; }
	}
}
