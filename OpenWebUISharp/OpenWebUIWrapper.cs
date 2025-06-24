using OpenWebUISharp.Models;
using OpenWebUISharp.Models.ChatCompletions;
using OpenWebUISharp.Models.ChatCompletions.API;
using OpenWebUISharp.Models.Knowledgebases;
using OpenWebUISharp.Models.Knowledgebases.API;
using OpenWebUISharp.Models.Models;
using OpenWebUISharp.Models.Models.API;
using OpenWebUISharp.Models.Tools;
using SerializableHttps;
using SerializableHttps.AuthenticationMethods;
using System.Text.Json;

namespace OpenWebUISharp
{
	public class OpenWebUIWrapper : IOpenWebUIWrapper
	{
		public string Token { get; set; }
		public string APIURL { get; set; }

		private readonly SerializableHttpsClient _client;

		public OpenWebUIWrapper(string token, string apiUrl)
		{
			Token = token;
			APIURL = apiUrl;

			_client = new SerializableHttpsClient();
			_client.SetAuthentication(new JWTAuthenticationMethod(token));
		}

		#region Models

		public async Task<List<Model>> GetAllModels()
		{
			var result = await _client.GetAsync<GetAllModelsResponse>(APIURL + "/api/models");
			return result.Data;
		}

		#endregion

		#region ChatCompletion

		public async Task<Conversation> Query(Conversation conversation, string modelId, ConversationOptions? options = null)
		{
			if (options == null)
				options = new ConversationOptions();
			var request = new ChatCompletionRequest()
			{
				Model = modelId,
				Messages = conversation.Messages.Select(x => new ChatCompletionMessage() { Content = x.Message, Role = x.Role }).ToList(),
				Files = options.KnowledgebaseIDs.Select(x => new ChatCompletionFile() { ID = x, Legacy = true }).ToList(),
				ToolIDs = options.ToolIDs,
				Parameters = new ChatCompletionParameters()
				{
					Temperature = options.Temperature
				}
			};
			var response = await _client.PostAsync<ChatCompletionRequest, ChatCompletionResponse>(request, APIURL + "/api/chat/completions");
			if (response.Choices.Count == 0)
				throw new Exception("Invalid response from OpenWebUI!");
			var newMessage = new ConversationMessage()
			{
				Role = response.Choices[0].Message.Role,
				Message = response.Choices[0].Message.Content
			};
			if (response.Sources != null && response.Sources.Count > 0 && response.Sources[0].MetaData != null)
				newMessage.RAGFiles = response.Sources[0].MetaData!.Select(x => new ConversationMessageRAGFile() { ID = x.ID, Name = x.Name, Score = x.Score }).ToList();
			if (options.RemoveThinking)
				newMessage.Message = RemoveThinking(newMessage.Message);
			conversation.Messages.Add(newMessage);
			return conversation;
		}

		private string RemoveThinking(string text)
		{
			var thinkStart = text.IndexOf("<think>");
			var thinkEnd = text.IndexOf("</think>");

			if (thinkStart != -1 && thinkEnd != -1 && thinkEnd > thinkStart)
				text = text.Remove(thinkStart, thinkEnd - thinkStart + 8);
			return text;
		}

		#endregion

		#region Knowledge base

		public async Task<List<KnowledgebaseModel>> GetAllKnowledgebases()
		{
			var response = await _client.GetAsync<List<OpenWebUIKnowledgebaseModel>>(APIURL + "/api/v1/knowledge/");
			var returnList = new List<KnowledgebaseModel>();
			foreach (var item in response)
				returnList.Add(Convert(item));
			return returnList;
		}

		public async Task<KnowledgebaseModel> GetKnowledgebase(string id)
		{
			var response = await _client.GetAsync<OpenWebUIKnowledgebaseModel>(APIURL + "/api/v1/knowledge/" + id);
			return Convert(response);
		}

		public async Task<KnowledgebaseModel> CreateKnowledgebase(string name)
		{
			var response = await _client.PostAsync<CreateKnowledgebaseInput, OpenWebUIKnowledgebaseModel>(
				new CreateKnowledgebaseInput()
				{
					Name = name,
					Description = "New Knowledgebase!"
				},
				APIURL + "/api/v1/knowledge/create");
			return Convert(response);
		}

		public async Task DeleteKnowledgebase(KnowledgebaseModel knowledgebase)
		{
			foreach (var file in knowledgebase.Files)
				await RemoveFileFromCollection(file.ID, knowledgebase);
			await _client.DeleteAsync(APIURL + "/api/v1/knowledge/" + knowledgebase.ID + "/delete");
		}

		public async Task AddFileToKnowledgebase(string text, KnowledgebaseModel collection, string fileName)
		{
			var file = await UploadFile(text, fileName);
			await _client.PostAsync<CreateKnowledgebaseFileInput, EmptyModel>(
				new CreateKnowledgebaseFileInput()
				{
					FileID = file.ID,
				},
				APIURL + "/api/v1/knowledge/" + collection.ID + "/file/add");
		}

		public async Task RemoveFileFromCollection(string fileID, KnowledgebaseModel collection)
		{
			await _client.PostAsync<CreateKnowledgebaseFileInput, EmptyModel>(
				new CreateKnowledgebaseFileInput()
				{
					FileID = fileID,
				},
				APIURL + "/api/v1/knowledge/" + collection.ID + "/file/remove");
		}

		private async Task<OpenWebUIFileModel> UploadFile(string text, string fileName)
		{
			var content = new MultipartFormDataContent();
			content.Add(new StreamContent(GenerateStreamFromString(text)), "file", fileName);
			var client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
			var fileResponse = await client.PostAsync(APIURL + "/api/v1/files/", content);
			var file = JsonSerializer.Deserialize<OpenWebUIFileModel>(await fileResponse.Content.ReadAsStringAsync());
			if (file == null)
				throw new Exception("OpenWebUI did not respond correctly!");
			return file;
		}

		private static MemoryStream GenerateStreamFromString(string s)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}

		private KnowledgebaseModel Convert(OpenWebUIKnowledgebaseModel item) => new KnowledgebaseModel()
		{
			ID = item.ID,
			Name = item.Name,
			Files = item.Files.Select(x => new KnowledgebaseFile()
			{
				ID = x.ID,
				Name = x.MetaData.Name
			}).ToList()
		};

		#endregion

		#region Tools

		public async Task<List<ToolModel>> GetAllTools() => await _client.GetAsync<List<ToolModel>>(APIURL + "/api/v1/tools/");

		#endregion
	}
}
