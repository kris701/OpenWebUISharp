namespace OpenWebUISharp.Tests
{
	[TestClass]
	public class ModelsTests : BaseModelTests
	{
		[TestMethod]
		[DataRow("ben1t0/tiny-llm:latest")]
		public async Task Can_PullModel(string targetModel)
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var models = await wrapper.Models.GetAll();

			// ACT
			var model = await wrapper.Models.Pull(targetModel);

			// ASSERT
			models = await wrapper.Models.GetAll();
			Assert.IsTrue(models.Any(x => x.Name == targetModel));

			await wrapper.Models.DeleteByID(model.ID);
		}
	}
}
