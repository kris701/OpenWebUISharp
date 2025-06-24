using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions.API
{
	internal class ChatCompletionParameters
	{
		[JsonPropertyName("temperature")]
		public double Temperature { get; set; }
	}
}
