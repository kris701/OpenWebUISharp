using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Knowledgebases.API
{
	internal class OpenWebUIKnowledgebaseModel
	{
		[JsonPropertyName("id")]
		public string ID { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("created_at")]
		public long CreatedAt { get; set; }
		[JsonPropertyName("updated_at")]
		public long UpdatedAt { get; set; }
		[JsonPropertyName("files")]
		public List<OpenWebUIFileModel> Files { get; set; }
	}
}
