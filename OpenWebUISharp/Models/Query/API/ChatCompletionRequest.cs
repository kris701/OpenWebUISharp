using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Query.API
{
	internal class ChatCompletionRequest
	{
		[JsonPropertyName("model")]
		public string Model { get; set; }
		[JsonPropertyName("messages")]
		public List<ChatCompletionMessage> Messages { get; set; }
		[JsonPropertyName("files")]
		public List<ChatCompletionFile>? Files { get; set; } = null;
		[JsonPropertyName("tool_ids")]
		public List<string>? ToolIDs { get; set; } = null;
		[JsonPropertyName("params")]
		public ChatCompletionParameters? Parameters { get; set; } = null;
		[JsonPropertyName("features")]
		public ChatCompletionFeatures? Features { get; set; } = new ChatCompletionFeatures();
		[JsonPropertyName("background_tasks")]
		public ChatCompletionBackgroundTasks? BackgroundTasks { get; set; } = new ChatCompletionBackgroundTasks();
		[JsonPropertyName("format")]
		public object? Format { get; set; } = null;
	}
}
