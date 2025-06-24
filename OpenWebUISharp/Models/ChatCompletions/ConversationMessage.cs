using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions
{
	/// <summary>
	/// A message in a conversation
	/// </summary>
	public class ConversationMessage
	{
		/// <summary>
		/// The role of the message
		/// </summary>
		[JsonPropertyName("role")]
		public string Role { get; set; } = "";
		/// <summary>
		/// The message body
		/// </summary>
		[JsonPropertyName("message")]
		public string Message { get; set; } = "";
		/// <summary>
		/// A list of what knowledgebase files was used in generating this message.
		/// This is only usable as output, setting this property yourself does nothing.
		/// </summary>
		[JsonPropertyName("ragFiles")]
		public List<ConversationMessageRAGFile>? RAGFiles { get; set; } = null;

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="role"></param>
		/// <param name="message"></param>
		public ConversationMessage(string role, string message)
		{
			Role = role;
			Message = message;
		}
	}
}
