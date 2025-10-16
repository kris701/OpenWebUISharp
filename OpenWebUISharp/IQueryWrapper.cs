namespace OpenWebUISharp
{
	/// <summary>
	/// Interface for the OpenWebUI query API
	/// </summary>
	public interface IQueryWrapper
	{
		/// <summary>
		/// Query a given model with a simple text string
		/// </summary>
		/// <param name="text"></param>
		/// <param name="modelId"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public Task<ConversationMessage> Query(string text, string modelId, ConversationOptions? options = null);
		/// <summary>
		/// Query a given model with a conversation.
		/// </summary>
		/// <param name="conversation">A list of messages and roles that have occured</param>
		/// <param name="modelId">The ID of the model you want to use</param>
		/// <param name="options">Optional set of options you can use.</param>
		/// <returns></returns>
		public Task<ConversationMessage> Query(Conversation conversation, string modelId, ConversationOptions? options = null);
		/// <summary>
		/// Query a given model, and force it to output the response in a JSON object that can be deserialized into <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">Some non-nullable json serialisable object</typeparam>
		/// <param name="text">Your query</param>
		/// <param name="modelId">The ID of the model you want to use</param>
		/// <param name="options">Optional set of options you can use.</param>
		/// <returns></returns>
		public Task<T> QueryToObject<T>(string text, string modelId, ConversationOptions? options = null) where T : notnull;
		/// <summary>
		/// Query a given model, and force it to output the response in a JSON object that can be deserialized into <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">Some non-nullable json serialisable object</typeparam>
		/// <param name="conversation">A list of messages and roles that have occured</param>
		/// <param name="modelId">The ID of the model you want to use</param>
		/// <param name="options">Optional set of options you can use.</param>
		/// <returns></returns>
		public Task<T> QueryToObject<T>(Conversation conversation, string modelId, ConversationOptions? options = null) where T : notnull;
	}
}
