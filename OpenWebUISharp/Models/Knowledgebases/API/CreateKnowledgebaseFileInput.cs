using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Knowledgebases.API
{
	internal class CreateKnowledgebaseFileInput
	{
		[JsonPropertyName("file_id")]
		public string FileID { get; set; }
		[JsonPropertyName("body")]
		public string Body { get; set; }
	}
}
