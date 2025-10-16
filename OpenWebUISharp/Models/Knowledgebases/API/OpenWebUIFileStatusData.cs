using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Knowledgebases.API
{
	internal class OpenWebUIFileStatusData
	{
		[JsonPropertyName("status")]
		public string Status { get; set; }
	}
}
