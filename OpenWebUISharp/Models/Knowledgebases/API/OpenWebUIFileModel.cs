using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Knowledgebases.API
{
	internal class OpenWebUIFileModel
	{
		[JsonPropertyName("id")]
		public string ID { get; set; }
		[JsonPropertyName("meta")]
		public OpenWebUIFileModelMetaData MetaData { get; set; }
	}
}
