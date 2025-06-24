using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Models
{
	/// <summary>
	/// A model in OpenWebUI
	/// </summary>
	public class Model
	{
		/// <summary>
		/// ID of the model
		/// </summary>
		[JsonPropertyName("id")]
		public string ID { get; set; }
		/// <summary>
		/// Name of the model
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="iD"></param>
		/// <param name="name"></param>
		public Model(string iD, string name)
		{
			ID = iD;
			Name = name;
		}
	}
}
