using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions
{
	/// <summary>
	/// Conversation object with a set of messages
	/// </summary>
	public class Conversation
	{
		/// <summary>
		/// The ordered list of messages
		/// </summary>
		[JsonPropertyName("messages")]
		public List<ConversationMessage> Messages { get; set; }

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="messages"></param>
		public Conversation(List<ConversationMessage> messages)
		{
			Messages = messages;
		}
	}
}
