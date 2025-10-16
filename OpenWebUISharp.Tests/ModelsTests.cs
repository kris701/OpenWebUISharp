namespace OpenWebUISharp.Tests
{
	[TestClass]
	public class ModelsTests : BaseModelTests
	{
		[ClassInitialize]
		public static async Task ClassInit(TestContext context)
		{
			await DeleteAllModels();
		}

		[TestMethod]
		[DataRow("ben1t0/tiny-llm:latest")]
		public async Task Can_PullModel(string targetModel)
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var models = await wrapper.Models.GetAll();
			Assert.IsFalse(models.Any(x => x.Name == targetModel));

			// ACT
			var model = await wrapper.Models.Pull(targetModel);

			// ASSERT
			models = await wrapper.Models.GetAll();
			Assert.IsTrue(models.Any(x => x.Name == targetModel));

			await wrapper.Models.DeleteByID(model.ID);
		}

		[ClassCleanup(ClassCleanupBehavior.EndOfClass)]
		public static async Task ClassCleanup()
		{
			await DeleteAllModels();
		}
	}
}
