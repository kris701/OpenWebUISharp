using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Knowledgebases.API
{
	internal class OpenWebUIFileModelMetaData
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
	}
}
