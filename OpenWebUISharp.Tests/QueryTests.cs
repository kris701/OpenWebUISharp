using OpenWebUISharp.Models.Query;

namespace OpenWebUISharp.Tests
{
	[TestClass]
	public class QueryTests : BaseModelTests
	{
		private static readonly string _targetModel = "ben1t0/tiny-llm:latest";

		[ClassInitialize]
		public static async Task ClassInit(TestContext context)
		{
			await DeleteAllModels();
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			await wrapper.Models.Pull(_targetModel);
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
					RemoveThinking = false
				});

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Message != "");
		}

		[ClassCleanup]
		public static async Task ClassCleanup()
		{
			await DeleteAllModels();
		}
	}
}
