using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Query.API
{
	internal class ChatCompletionParameters
	{
		[JsonPropertyName("temperature")]
		public double Temperature { get; set; }
	}
}
