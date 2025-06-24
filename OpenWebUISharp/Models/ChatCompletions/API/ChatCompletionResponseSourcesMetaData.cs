using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions.API
{
	public class ChatCompletionResponseSourcesMetaData
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("score")]
		public double Score { get; set; }
	}
}
