namespace OpenWebUISharp.Models.ChatCompletions
{
	public class ConversationOptions
	{
		public List<string> ToolIDs { get; set; } = new List<string>();
		public List<string> CollectionIDs { get; set; } = new List<string>();

		public double Temperature { get; set; } = 1;
	}
}
