using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Query.API
{
	internal class ChatCompletionFile
	{
		[JsonPropertyName("id")]
		public Guid ID { get; set; }
		[JsonPropertyName("type")]
		public string Type { get; set; }
	}
}
