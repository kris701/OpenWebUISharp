using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions.API
{
	internal class ChatCompletionFile
	{
		[JsonPropertyName("id")]
		public Guid ID { get; set; }
		[JsonPropertyName("legacy")]
		public bool Legacy { get; set; }
	}
}
