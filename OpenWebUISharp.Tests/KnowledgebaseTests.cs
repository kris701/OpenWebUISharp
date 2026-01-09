namespace OpenWebUISharp.Tests
{
	[TestClass]
	public class KnowledgebaseTests
	{
		[ClassInitialize]
		public static async Task ClassInit(TestContext context)
		{
			await DeleteAllKnowledgebases();
		}

		[TestMethod]
		public async Task Can_AddKnowledgebase()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);

			// ACT
			var newKnowledgebase = await wrapper.Knowledgebase.Add("addknowledgebase");

			// ASSERT
			Assert.IsNotNull(newKnowledgebase);
			var existing = await wrapper.Knowledgebase.GetByID(newKnowledgebase.ID);
			Assert.IsNotNull(existing);
			Assert.AreEqual(newKnowledgebase.ID, existing.ID);
			Assert.AreEqual(newKnowledgebase.Name, existing.Name);
			Assert.AreEqual(newKnowledgebase.CreatedAt, existing.CreatedAt);
			Assert.AreEqual(newKnowledgebase.UpdatedAt, existing.UpdatedAt);

			await wrapper.Knowledgebase.Delete(newKnowledgebase.ID);
		}

		[TestMethod]
		public async Task Can_DeleteKnowledgebase()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var newKnowledgebase = await wrapper.Knowledgebase.Add("deleteknowledgebase");
			var existing = await wrapper.Knowledgebase.GetAll();
			Assert.IsTrue(existing.Any(x => x.ID == newKnowledgebase.ID));

			// ACT
			await wrapper.Knowledgebase.Delete(newKnowledgebase.ID);

			// ASSERT
			existing = await wrapper.Knowledgebase.GetAll();
			Assert.IsFalse(existing.Any(x => x.ID == newKnowledgebase.ID));
		}

		[TestMethod]
		public async Task Can_GetKnowledgebaseByID()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var newKnowledgebase = await wrapper.Knowledgebase.Add("getknowledgebasebyid");

			// ACT
			var existing = await wrapper.Knowledgebase.GetByID(newKnowledgebase.ID);

			// ASSERT
			Assert.IsNotNull(newKnowledgebase);
			Assert.IsNotNull(existing);
			Assert.AreEqual(newKnowledgebase.ID, existing.ID);
			Assert.AreEqual(newKnowledgebase.Name, existing.Name);
			Assert.AreEqual(newKnowledgebase.CreatedAt, existing.CreatedAt);
			Assert.AreEqual(newKnowledgebase.UpdatedAt, existing.UpdatedAt);

			await wrapper.Knowledgebase.Delete(newKnowledgebase.ID);
		}

		[TestMethod]
		public async Task Can_GetKnowledgebaseByName()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var newKnowledgebase = await wrapper.Knowledgebase.Add("getknowledgebasebyname");

			// ACT
			var existing = await wrapper.Knowledgebase.GetByName(newKnowledgebase.Name);

			// ASSERT
			Assert.IsNotNull(newKnowledgebase);
			Assert.IsNotNull(existing);
			Assert.AreEqual(newKnowledgebase.ID, existing.ID);
			Assert.AreEqual(newKnowledgebase.Name, existing.Name);
			Assert.AreEqual(newKnowledgebase.CreatedAt, existing.CreatedAt);
			Assert.AreEqual(newKnowledgebase.UpdatedAt, existing.UpdatedAt);

			await wrapper.Knowledgebase.Delete(newKnowledgebase.ID);
		}

		[TestMethod]
		public async Task Can_AddFilesToKnowledgebase()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var newKnowledgebase = await wrapper.Knowledgebase.Add("addfilesknowledgebase");
			var existing = await wrapper.Knowledgebase.GetByID(newKnowledgebase.ID);
			Assert.IsEmpty(existing.Files);

			// ACT
			await wrapper.Knowledgebase.AddFile("add file test", newKnowledgebase.ID, "add.txt");

			// ASSERT
			existing = await wrapper.Knowledgebase.GetByID(newKnowledgebase.ID);
			Assert.HasCount(1, existing.Files);
			Assert.AreEqual("add.txt", existing.Files[0].Name);

			await wrapper.Knowledgebase.DeleteFile(existing.Files[0].ID, existing.ID);
			await wrapper.Knowledgebase.Delete(newKnowledgebase.ID);
		}

		[TestMethod]
		public async Task Can_RemoveFilesToKnowledgebase()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var newKnowledgebase = await wrapper.Knowledgebase.Add("removefilesknowledgebase");
			var existing = await wrapper.Knowledgebase.GetByID(newKnowledgebase.ID);
			Assert.IsTrue(existing.Files.Count == 0);
			await wrapper.Knowledgebase.AddFile("delete file test", newKnowledgebase.ID, "delete.txt");
			existing = await wrapper.Knowledgebase.GetByID(newKnowledgebase.ID);
			Assert.IsTrue(existing.Files.Count == 1);

			// ACT
			await wrapper.Knowledgebase.DeleteFile(existing.Files[0].ID, existing.ID);

			// ASSERT
			existing = await wrapper.Knowledgebase.GetByID(newKnowledgebase.ID);
			Assert.IsTrue(existing.Files.Count == 0);

			await wrapper.Knowledgebase.Delete(newKnowledgebase.ID);
		}

		[ClassCleanup]
		public static async Task ClassCleanup()
		{
			await DeleteAllKnowledgebases();
		}

		private static async Task DeleteAllKnowledgebases()
		{
			var wrapper = new KnowledgebaseWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var knowledgebases = await wrapper.GetAll();
			foreach (var knowledgebase in knowledgebases)
			{
				foreach (var file in knowledgebase.Files)
					await wrapper.DeleteFile(file.ID, knowledgebase.ID);
				await wrapper.Delete(knowledgebase.ID);
			}
		}
	}
}
