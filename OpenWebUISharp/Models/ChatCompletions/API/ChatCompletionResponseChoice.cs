using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions.API
{
	internal class ChatCompletionResponseChoice
	{
		[JsonPropertyName("message")]
		public ChatCompletionMessage Message { get; set; }
	}
}
