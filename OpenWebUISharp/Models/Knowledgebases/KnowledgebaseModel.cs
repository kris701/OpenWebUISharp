using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Knowledgebases
{
	/// <summary>
	/// A knowledgebase
	/// </summary>
	public class KnowledgebaseModel
	{
		/// <summary>
		/// ID of the knowledgebase
		/// </summary>
		[JsonPropertyName("id")]
		public string ID { get; set; }
		/// <summary>
		/// Name of the knowledgebase
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }
		/// <summary>
		/// Set of files that is in the knowledgebase.
		/// </summary>
		[JsonPropertyName("files")]
		public List<KnowledgebaseFile> Files { get; set; }

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="iD"></param>
		/// <param name="name"></param>
		/// <param name="files"></param>
		public KnowledgebaseModel(string iD, string name, List<KnowledgebaseFile> files)
		{
			ID = iD;
			Name = name;
			Files = files;
		}
	}
}
