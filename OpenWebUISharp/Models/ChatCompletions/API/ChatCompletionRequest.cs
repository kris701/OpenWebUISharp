using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions.API
{
	internal class ChatCompletionRequest
	{
		[JsonPropertyName("model")]
		public string Model { get; set; }
		[JsonPropertyName("messages")]
		public List<ChatCompletionMessage> Messages { get; set; }
		[JsonPropertyName("files")]
		public List<ChatCompletionFile> Files { get; set; }
		[JsonPropertyName("tool_ids")]
		public List<string> ToolIDs { get; set; }
		[JsonPropertyName("params")]
		public ChatCompletionParameters Parameters { get; set; }
	}
}
