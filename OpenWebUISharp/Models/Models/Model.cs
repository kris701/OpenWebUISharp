using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Models
{
	public class Model
	{
		[JsonPropertyName("id")]
		public string ID { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; }
	}
}
