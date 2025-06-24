using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions
{
	/// <summary>
	/// An object representing a file that was used in generating a conversation message
	/// </summary>
	public class ConversationMessageRAGFile
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
		/// A score of how "close" the file was to the query. 1 is the highest 0 the lowest.
		/// </summary>
		[JsonPropertyName("score")]
		public double Score { get; set; }

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="iD"></param>
		/// <param name="name"></param>
		/// <param name="score"></param>
		public ConversationMessageRAGFile(string iD, string name, double score)
		{
			ID = iD;
			Name = name;
			Score = score;
		}
	}
}
