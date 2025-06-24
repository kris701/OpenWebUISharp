using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions.API
{
	public class ChatCompletionParameters
	{
		[JsonPropertyName("temperature")]
		public double Temperature { get; set; }
	}
}
