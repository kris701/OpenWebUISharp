using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Query.API
{
	internal class ChatCompletionResponseChoice
	{
		[JsonPropertyName("message")]
		public ChatCompletionMessage Message { get; set; }
	}
}
