using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Knowledgebases.API
{
	public class OpenWebUIFileModelMetaData
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
	}
}
