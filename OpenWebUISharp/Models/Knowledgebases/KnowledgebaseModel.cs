using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Knowledgebases
{
	public class KnowledgebaseModel
	{
		[JsonPropertyName("id")]
		public string ID { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("files")]
		public List<KnowledgebaseFile> Files { get; set; }
	}
}
