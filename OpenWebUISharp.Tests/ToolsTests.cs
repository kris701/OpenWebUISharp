namespace OpenWebUISharp.Tests
{
	[TestClass]
	public class ToolsTests
	{
		private static readonly string _addToolContent = "class Tools:\r\n    def __init__(self):\r\n        pass";

		[ClassInitialize]
		public static async Task ClassInit(TestContext context)
		{
			await DeleteAllTools();
		}

		[TestMethod]
		public async Task Can_AddTool()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var tools = await wrapper.Tools.GetAll();
			Assert.IsFalse(tools.Any(x => x.Name == "testtool"));

			// ACT
			var result = await wrapper.Tools.Add("testtool", "desc", _addToolContent);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.AreEqual("testtool", result.ID);
			Assert.AreEqual("testtool", result.Name);
			Assert.AreEqual(_addToolContent, result.Content);
			Assert.AreEqual("desc", result.Description);
			tools = await wrapper.Tools.GetAll();
			Assert.IsTrue(tools.Any(x => x.Name == "testtool"));
		}

		[TestMethod]
		public async Task Can_DeleteTool()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var tools = await wrapper.Tools.GetAll();
			Assert.IsFalse(tools.Any(x => x.Name == "testtool2"));
			var tool = await wrapper.Tools.Add("testtool2", "desc", _addToolContent);
			tools = await wrapper.Tools.GetAll();
			Assert.IsTrue(tools.Any(x => x.Name == "testtool2"));

			// ACT
			await wrapper.Tools.Delete(tool.ID);

			// ASSERT
			tools = await wrapper.Tools.GetAll();
			Assert.IsFalse(tools.Any(x => x.Name == "testtool2"));
		}

		[ClassCleanup(ClassCleanupBehavior.EndOfClass)]
		public static async Task ClassCleanup()
		{
			await DeleteAllTools();
		}

		private static async Task DeleteAllTools()
		{
			var wrapper = new ToolsWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var tools = await wrapper.GetAll();
			foreach (var tool in tools)
				await wrapper.Delete(tool.ID);
		}
	}
}
