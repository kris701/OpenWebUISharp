using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Knowledgebases.API
{
	internal class CreateKnowledgebaseInput
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("description")]
		public string Description { get; set; }
	}
}
