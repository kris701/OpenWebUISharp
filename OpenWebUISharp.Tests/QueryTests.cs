using OpenWebUISharp.Models.Query;

namespace OpenWebUISharp.Tests
{
	[TestClass]
	public class QueryTests : BaseModelTests
	{
		private static readonly string _targetModel = "ben1t0/tiny-llm:latest";
		private static readonly string _targetModel2 = "gemma3:1b";

		[ClassInitialize]
		public static async Task ClassInit(TestContext context)
		{
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			await wrapper.Models.Pull(_targetModel);
			await wrapper.Models.Pull(_targetModel2);
		}

		[TestMethod]
		public async Task Can_QueryModel_Simple()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			await wrapper.Models.Pull(_targetModel);

			// ACT
			var result = await wrapper.Query.Query(
				"hello",
				_targetModel);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Message != "");
		}

		[TestMethod]
		public async Task Can_QueryModel_Conversation()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);

			var conversation = new Conversation(
				new List<ConversationMessage>()
				{
					new ConversationMessage("user", "hello"),
					new ConversationMessage("assistant", "hello there!"),
					new ConversationMessage("user", "how are you?")
				});

			// ACT
			var result = await wrapper.Query.Query(
				conversation,
				_targetModel);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Message != "");
		}

		[TestMethod]
		public async Task Can_QueryModel_Options()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);

			// ACT
			var result = await wrapper.Query.Query(
				"hello",
				_targetModel,
				new ConversationOptions()
				{
					Temperature = 0.2,
					SystemPrompt = "you are a helper",
					RemoveThinking = false
				});

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Message != "");
		}

		[TestMethod]
		public async Task Can_QueryModel_Options_SystemPrompt()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);

			// ACT
			var result = await wrapper.Query.Query(
				"What is the ticket id?",
				_targetModel2,
				new ConversationOptions()
				{
					SystemPrompt = "You are assisting with the ticket id 'ID33602'"
				});

			// ASSERT
			Assert.IsNotNull(result);
			Assert.Contains("ID33602", result.Message);
		}

		[TestMethod]
		public async Task Can_QueryModel_Options_Knowledgebase()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var newKnowledgebase = await wrapper.Knowledgebase.Add("queryknowledgebase");
			await wrapper.Knowledgebase.AddFile("A52LG = YES", newKnowledgebase.ID, "add.txt");
			newKnowledgebase = await wrapper.Knowledgebase.GetByID(newKnowledgebase.ID);

			// ACT
			var result = await wrapper.Query.Query(
				"What does A52LG mean?",
				_targetModel2,
				new ConversationOptions()
				{
					KnowledgebaseIDs = new List<Guid>() { newKnowledgebase.ID }
				});

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.RAGFiles);
			Assert.AreNotEqual("", result.Message);
			Assert.IsTrue(result.RAGFiles.Any(x => x.ID == newKnowledgebase.Files[0].ID.ToString()));

			await wrapper.Knowledgebase.DeleteFile(newKnowledgebase.Files[0].ID, newKnowledgebase.ID);
			await wrapper.Knowledgebase.Delete(newKnowledgebase.ID);
		}
	}
}
