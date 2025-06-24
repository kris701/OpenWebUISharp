using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Knowledgebases.API
{
	internal class OpenWebUIFileModel
	{
		[JsonPropertyName("id")]
		public string ID { get; set; }
		[JsonPropertyName("created_at")]
		public long CreatedAt { get; set; }
		[JsonPropertyName("updated_at")]
		public long UpdatedAt { get; set; }
		[JsonPropertyName("meta")]
		public OpenWebUIFileModelMetaData MetaData { get; set; }
	}
}
