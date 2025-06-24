using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Knowledgebases
{
	/// <summary>
	/// A file in a knowledgebase collection
	/// </summary>
	public class KnowledgebaseFile
	{
		/// <summary>
		/// ID of the file
		/// </summary>
		[JsonPropertyName("id")]
		public string ID { get; set; }
		/// <summary>
		/// Name of the file
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="iD"></param>
		/// <param name="name"></param>
		public KnowledgebaseFile(string iD, string name)
		{
			ID = iD;
			Name = name;
		}
	}
}
