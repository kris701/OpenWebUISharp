using NJsonSchema;
using OpenWebUISharp.Models.Query;
using OpenWebUISharp.Models.Query.API;
using SerializableHttps;
using SerializableHttps.AuthenticationMethods;
using System.Text.Json;

namespace OpenWebUISharp
{
	/// <summary>
	/// Implementation of the wrapper
	/// </summary>
	public class QueryWrapper : IQueryWrapper
	{
		/// <summary>
		/// The JWT token you can find in the OpenWebUI settings
		/// </summary>
		public string Token { get; set; }
		/// <summary>
		/// The URL (or IP) to OpenWebUI
		/// </summary>
		public string APIURL { get; set; }


		private readonly SerializableHttpsClient _client;

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="token"></param>
		/// <param name="apiUrl"></param>
		public QueryWrapper(string token, string apiUrl)
		{
			Token = token;
			APIURL = apiUrl;

			_client = new SerializableHttpsClient();
			_client.SetAuthentication(new JWTAuthenticationMethod(token));
		}

		/// <summary>
		/// Query a given model with a simple text string
		/// </summary>
		/// <param name="text"></param>
		/// <param name="modelId"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public async Task<ConversationMessage> Query(string text, string modelId, ConversationOptions? options = null)
		{
			return await Query(
				new Conversation(new List<ConversationMessage>()
				{
					new ConversationMessage("user", text)
				}),
				modelId,
				options);
		}

		/// <summary>
		/// Query a given model with a conversation.
		/// </summary>
		/// <param name="conversation">A list of messages and roles that have occured</param>
		/// <param name="modelId">The ID of the model you want to use</param>
		/// <param name="options">Optional set of options you can use.</param>
		/// <returns></returns>
		public async Task<ConversationMessage> Query(Conversation conversation, string modelId, ConversationOptions? options = null)
		{
			if (options == null)
				options = new ConversationOptions();
			var messages = conversation.Messages.Select(x => new ChatCompletionMessage() { Content = x.Message, Role = x.Role }).ToList();
			if (options.SystemPrompt != null)
			{
				if (messages.Count == 0)
					messages.Add(new ChatCompletionMessage() { Content = options.SystemPrompt, Role = "system" });
				else if (messages[0].Role == "system")
					messages[0].Content = options.SystemPrompt;
				else
					messages.Insert(0, new ChatCompletionMessage() { Content = options.SystemPrompt, Role = "system" });
			}
			var request = new ChatCompletionRequest()
			{
				Model = modelId,
				Messages = messages,
				Files = options.KnowledgebaseIDs.Count > 0 ? options.KnowledgebaseIDs.Select(x => new ChatCompletionFile() { ID = x, Type = "collection" }).ToList() : null,
				ToolIDs = options.ToolIDs.Count > 0 ? options.ToolIDs : null,
				Parameters = new ChatCompletionParameters()
				{
					Temperature = options.Temperature,
					SystemPrompt = options.SystemPrompt
				}
			};
			var tst = JsonSerializer.Serialize(request);
			var response = await _client.PostAsync<ChatCompletionRequest, ChatCompletionResponse>(request, APIURL + "/api/chat/completions");
			if (response.Choices.Count == 0)
				throw new Exception("Invalid response from OpenWebUI!");
			var newMessage = new ConversationMessage(
				response.Choices[0].Message.Role,
				response.Choices[0].Message.Content);
			if (response.Sources != null && response.Sources.Count > 0 && response.Sources[0].MetaData != null)
				newMessage.RAGFiles = response.Sources[0].MetaData!.Select(x => new ConversationMessageRAGFile(x.ID, x.Name, x.Score)).ToList();
			if (options.RemoveThinking)
				newMessage.Message = RemoveThinking(newMessage.Message);
			return newMessage;
		}

		/// <summary>
		/// Query a given model, and force it to output the response in a JSON object that can be deserialized into <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">Some non-nullable json serialisable object</typeparam>
		/// <param name="text">Your query</param>
		/// <param name="modelId">The ID of the model you want to use</param>
		/// <param name="options">Optional set of options you can use.</param>
		/// <returns></returns>
		public async Task<T> QueryToObject<T>(string text, string modelId, ConversationOptions? options = null) where T : notnull
		{
			return await QueryToObject<T>(
				new Conversation(new List<ConversationMessage>()
				{
					new ConversationMessage("user", text)
				}),
				modelId,
				options);
		}

		/// <summary>
		/// Query a given model, and force it to output the response in a JSON object that can be deserialized into <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">Some non-nullable json serialisable object</typeparam>
		/// <param name="conversation">A list of messages and roles that have occured</param>
		/// <param name="modelId">The ID of the model you want to use</param>
		/// <param name="options">Optional set of options you can use.</param>
		/// <returns></returns>
		public async Task<T> QueryToObject<T>(Conversation conversation, string modelId, ConversationOptions? options = null) where T : notnull
		{
			if (options == null)
				options = new ConversationOptions();
			var messages = conversation.Messages.Select(x => new ChatCompletionMessage() { Content = x.Message, Role = x.Role }).ToList();
			if (options.SystemPrompt != null)
			{
				if (messages.Count == 0)
					messages.Add(new ChatCompletionMessage() { Content = options.SystemPrompt, Role = "system" });
				else if (messages[0].Role == "system")
					messages[0].Content = options.SystemPrompt;
				else
					messages.Insert(0, new ChatCompletionMessage() { Content = options.SystemPrompt, Role = "system" });
			}
			var request = new ChatCompletionRequest()
			{
				Model = modelId,
				Messages = messages,
				Files = options.KnowledgebaseIDs.Count > 0 ? options.KnowledgebaseIDs.Select(x => new ChatCompletionFile() { ID = x, Type = "collection" }).ToList() : null,
				ToolIDs = options.ToolIDs.Count > 0 ? options.ToolIDs : null,
				Parameters = new ChatCompletionParameters()
				{
					Temperature = options.Temperature,
					SystemPrompt = options.SystemPrompt
				},
				Format = JsonSerializer.Deserialize<object>(JsonSchema.FromType<T>().ToJson())
			};
			var response = await _client.PostAsync<ChatCompletionRequest, ChatCompletionResponse>(request, APIURL + "/api/chat/completions");
			if (response.Choices.Count == 0)
				throw new Exception("Invalid response from OpenWebUI!");
			var deserialized = JsonSerializer.Deserialize<T>(response.Choices[0].Message.Content);
			if (deserialized == null)
				throw new Exception("OpenWebUI did not respond with a valid JSON response!");
			return deserialized;
		}

		private string RemoveThinking(string text)
		{
			var thinkStart = text.IndexOf("<think>");
			var thinkEnd = text.IndexOf("</think>");

			if (thinkStart != -1 && thinkEnd != -1 && thinkEnd > thinkStart)
				text = text.Remove(thinkStart, thinkEnd - thinkStart + 8);
			return text;
		}
	}
}
