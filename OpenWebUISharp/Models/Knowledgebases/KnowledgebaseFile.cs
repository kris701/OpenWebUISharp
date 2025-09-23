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
		public Guid ID { get; set; }
		/// <summary>
		/// Name of the file
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Timestamp of knowledgebase file creation
		/// </summary>
		[JsonPropertyName("createdat")]
		public DateTime CreatedAt { get; set; }
		/// <summary>
		/// Timestamp of knowledgebase file update
		/// </summary>
		[JsonPropertyName("updatedat")]
		public DateTime UpdatedAt { get; set; }

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="iD"></param>
		/// <param name="name"></param>
		/// <param name="createdAt"></param>
		/// <param name="updatedAt"></param>
		public KnowledgebaseFile(Guid iD, string name, DateTime createdAt, DateTime updatedAt)
		{
			ID = iD;
			Name = name;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}
	}
}
