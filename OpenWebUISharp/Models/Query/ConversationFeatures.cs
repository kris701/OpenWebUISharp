using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Query
{
	/// <summary>
	/// OpenWebUI Features for chat completions
	/// </summary>
	public class ConversationFeatures
	{
		[JsonPropertyName("code_interpreter")]
		public bool CodeInterpreter { get; set; } = false;
		[JsonPropertyName("image_generation")]
		public bool ImageGeneration { get; set; } = false;
		[JsonPropertyName("memory")]
		public bool Memory { get; set; } = false;
		[JsonPropertyName("voice")]
		public bool Voice { get; set; } = false;
		[JsonPropertyName("web_search")]
		public bool WebSearch { get; set; } = false;
	}
}
