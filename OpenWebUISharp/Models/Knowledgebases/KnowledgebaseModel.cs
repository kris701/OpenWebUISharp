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
		public Guid ID { get; set; }
		/// <summary>
		/// Name of the knowledgebase
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }
		/// <summary>
		/// Description of the knowledgebase
		/// </summary>
		[JsonPropertyName("description")]
		public string Description { get; set; }
		/// <summary>
		/// Set of files that is in the knowledgebase.
		/// </summary>
		[JsonPropertyName("files")]
		public List<KnowledgebaseFile> Files { get; set; }

		/// <summary>
		/// Timestamp of knowledgebase creation
		/// </summary>
		[JsonPropertyName("updatedat")]
		public DateTime CreatedAt { get; set; }
		/// <summary>
		/// Timestamp of knowledgebase update
		/// </summary>
		[JsonPropertyName("updatedat")]
		public DateTime UpdatedAt { get; set; }

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="iD"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		/// <param name="files"></param>
		/// <param name="createdAt"></param>
		/// <param name="updatedAt"></param>
		public KnowledgebaseModel(Guid iD, string name, string description, List<KnowledgebaseFile> files, DateTime createdAt, DateTime updatedAt)
		{
			ID = iD;
			Name = name;
			Description = description;
			Files = files;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="other"></param>
		public KnowledgebaseModel(KnowledgebaseModel other)
		{
			ID = other.ID;
			Name = other.Name;
			Description = other.Description;
			Files = other.Files;
			CreatedAt = other.CreatedAt;
			UpdatedAt = other.UpdatedAt;
		}
	}
}
