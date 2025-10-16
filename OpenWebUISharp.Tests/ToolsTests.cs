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
			var tools = await wrapper.Tools.GetAllTools();
			Assert.IsFalse(tools.Any(x => x.Name == "testtool"));

			// ACT
			var result = await wrapper.Tools.AddTool("testtool", "desc", _addToolContent);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.AreEqual("testtool", result.ID);
			Assert.AreEqual("testtool", result.Name);
			Assert.AreEqual(_addToolContent, result.Content);
			Assert.AreEqual("desc", result.Description);
			tools = await wrapper.Tools.GetAllTools();
			Assert.IsTrue(tools.Any(x => x.Name == "testtool"));
		}

		[TestMethod]
		public async Task Can_DeleteTool()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var tools = await wrapper.Tools.GetAllTools();
			Assert.IsFalse(tools.Any(x => x.Name == "testtool2"));
			var tool = await wrapper.Tools.AddTool("testtool2", "desc", _addToolContent);
			tools = await wrapper.Tools.GetAllTools();
			Assert.IsTrue(tools.Any(x => x.Name == "testtool2"));

			// ACT
			await wrapper.Tools.DeleteTool(tool.ID);

			// ASSERT
			tools = await wrapper.Tools.GetAllTools();
			Assert.IsFalse(tools.Any(x => x.Name == "testtool2"));
		}

		[ClassCleanup]
		public static async Task ClassCleanup()
		{
			await DeleteAllTools();
		}

		private static async Task DeleteAllTools()
		{
			var wrapper = new ToolsWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var tools = await wrapper.GetAllTools();
			foreach (var tool in tools)
				await wrapper.DeleteTool(tool.ID);
		}
	}
}
