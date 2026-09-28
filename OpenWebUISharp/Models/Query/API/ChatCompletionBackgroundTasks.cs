using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Query.API
{
	internal class ChatCompletionBackgroundTasks
	{
		[JsonPropertyName("follow_up_generation")]
		public bool FollowUpGeneration { get; set; } = false;
		[JsonPropertyName("tags_generation")]
		public bool TagsGeneration { get; set; } = false;
		[JsonPropertyName("title_generation")]
		public bool TitleGeneration { get; set; } = false;
	}
}
