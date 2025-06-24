using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions.API
{
	public class ChatCompletionResponseChoice
	{
		[JsonPropertyName("message")]
		public ChatCompletionMessage Message { get; set; }
	}
}
