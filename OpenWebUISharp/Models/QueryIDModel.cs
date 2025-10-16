using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models
{
	internal class QueryIDModel
	{
		[JsonPropertyName("id")]
		public string ID { get; set; }
	}
}
