using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Knowledgebases
{
	public class KnowledgebaseFile
	{
		[JsonPropertyName("id")]
		public string ID { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; }
	}
}
