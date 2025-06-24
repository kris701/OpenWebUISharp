using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions.API
{
	public class ChatCompletionResponse
	{
		[JsonPropertyName("choices")]
		public List<ChatCompletionResponseChoice> Choices { get; set; }
		[JsonPropertyName("sources")]
		public List<ChatCompletionResponseSources> Sources { get; set; }
	}
}
