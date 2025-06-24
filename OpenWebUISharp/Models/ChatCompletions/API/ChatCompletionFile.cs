using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions.API
{
	public class ChatCompletionFile
	{
		[JsonPropertyName("id")]
		public string ID { get; set; }
		[JsonPropertyName("legacy")]
		public bool Legacy { get; set; }
	}
}
