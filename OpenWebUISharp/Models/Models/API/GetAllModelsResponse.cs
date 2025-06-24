using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Models.API
{
	internal class GetAllModelsResponse
	{
		[JsonPropertyName("data")]
		public List<Model> Data { get; set; }
	}
}
