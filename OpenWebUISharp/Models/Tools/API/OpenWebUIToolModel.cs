using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Tools.API
{
	internal class OpenWebUIToolModel
	{
		[JsonPropertyName("id")]
		public string ID { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("content")]
		public string Content { get; set; }
		[JsonPropertyName("meta")]
		public OpenWebUIToolMetaModel Meta { get; set; }
	}
}
