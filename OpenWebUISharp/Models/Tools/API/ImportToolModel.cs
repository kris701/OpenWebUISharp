using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Tools.API
{
	internal class ImportToolModel
	{
		[JsonPropertyName("url")]
		public string URL { get; set; }
	}
}
