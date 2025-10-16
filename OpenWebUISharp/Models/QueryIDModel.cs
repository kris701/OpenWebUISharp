using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models
{
	public class QueryIDModel
	{
		[JsonPropertyName("id")]
		public string ID { get; set; }
	}
}
