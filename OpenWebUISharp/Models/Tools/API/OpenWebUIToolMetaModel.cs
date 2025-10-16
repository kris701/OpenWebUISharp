using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Tools.API
{
	internal class OpenWebUIToolMetaModel
	{
		[JsonPropertyName("description")]
		public string Description { get; set; }
	}
}
