using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Knowledgebases.API
{
	internal class OpenWebUIKnowledgebaseModel
	{
		[JsonPropertyName("id")]
		public string ID { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("files")]
		public List<OpenWebUIFileModel> Files { get; set; }
	}
}
