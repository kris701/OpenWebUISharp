using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions.API
{
	internal class ChatCompletionResponseSources
	{
		[JsonPropertyName("metadata")]
		public List<ChatCompletionResponseSourcesMetaData>? MetaData { get; set; }
	}
}
