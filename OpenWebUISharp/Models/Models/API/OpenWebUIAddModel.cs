using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Models.API
{
	internal class OpenWebUIAddModel
	{
		[JsonPropertyName("model")]
		public string Model { get; set; }
	}
}
