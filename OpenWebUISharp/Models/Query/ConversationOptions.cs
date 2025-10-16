using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Query
{
	/// <summary>
	/// Set of optional settings you can use when querying a model
	/// </summary>
	public class ConversationOptions
	{
		/// <summary>
		/// Set of tools the model can use
		/// </summary>
		[JsonPropertyName("toolids")]
		public List<string> ToolIDs { get; set; } = new List<string>();
		/// <summary>
		/// Set of knowledgebase collections the model can use
		/// </summary>
		[JsonPropertyName("knowledgebaseids")]
		public List<Guid> KnowledgebaseIDs { get; set; } = new List<Guid>();

		/// <summary>
		/// Temperature to generate the response from
		/// </summary>
		[JsonPropertyName("temperature")]
		public double Temperature { get; set; } = 1;

		/// <summary>
		/// Some models have a "thinking" part of their response. Set this property to true to remove it.
		/// </summary>
		[JsonPropertyName("removethinking")]
		public bool RemoveThinking { get; set; } = false;
	}
}
