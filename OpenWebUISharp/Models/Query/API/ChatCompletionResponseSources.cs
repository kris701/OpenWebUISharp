using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Query.API
{
	internal class ChatCompletionResponseSources
	{
		[JsonPropertyName("metadata")]
		public List<ChatCompletionResponseSourcesMetaData>? MetaData { get; set; }
	}
}
