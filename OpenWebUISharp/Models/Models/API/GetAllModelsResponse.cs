using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Models.API
{
	public class GetAllModelsResponse
	{
		[JsonPropertyName("data")]
		public List<Model> Data { get; set; }
	}
}
