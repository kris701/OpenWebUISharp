using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Knowledgebases.API
{
	internal class OpenWebUIFileStatus
	{
		[JsonPropertyName("data")]
		public OpenWebUIFileStatusData Data { get; set; }
	}
}
