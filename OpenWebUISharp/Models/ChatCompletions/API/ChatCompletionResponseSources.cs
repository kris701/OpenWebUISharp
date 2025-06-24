using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions.API
{
	public class ChatCompletionResponseSources
	{
		[JsonPropertyName("metadata")]
		public List<ChatCompletionResponseSourcesMetaData>? MetaData { get; set; }
	}
}
