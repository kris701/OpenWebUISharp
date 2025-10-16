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
			var newKnowledgebase = await wrapper.Knowledgebase.CreateKnowledgebase("addknowledgebase");

			// ASSERT
			Assert.IsNotNull(newKnowledgebase);
			var existing = await wrapper.Knowledgebase.GetKnowledgebaseByID(newKnowledgebase.ID);
			Assert.IsNotNull(existing);
			Assert.AreEqual(newKnowledgebase.ID, existing.ID);
			Assert.AreEqual(newKnowledgebase.Name, existing.Name);
			Assert.AreEqual(newKnowledgebase.CreatedAt, existing.CreatedAt);
			Assert.AreEqual(newKnowledgebase.UpdatedAt, existing.UpdatedAt);

			await wrapper.Knowledgebase.DeleteKnowledgebase(newKnowledgebase.ID);
		}

		[TestMethod]
		public async Task Can_DeleteKnowledgebase()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var newKnowledgebase = await wrapper.Knowledgebase.CreateKnowledgebase("deleteknowledgebase");
			var existing = await wrapper.Knowledgebase.GetAllKnowledgebases();
			Assert.IsTrue(existing.Any(x => x.ID == newKnowledgebase.ID));

			// ACT
			await wrapper.Knowledgebase.DeleteKnowledgebase(newKnowledgebase.ID);

			// ASSERT
			existing = await wrapper.Knowledgebase.GetAllKnowledgebases();
			Assert.IsFalse(existing.Any(x => x.ID == newKnowledgebase.ID));
		}

		[TestMethod]
		public async Task Can_GetKnowledgebaseByID()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var newKnowledgebase = await wrapper.Knowledgebase.CreateKnowledgebase("getknowledgebasebyid");

			// ACT
			var existing = await wrapper.Knowledgebase.GetKnowledgebaseByID(newKnowledgebase.ID);

			// ASSERT
			Assert.IsNotNull(newKnowledgebase);
			Assert.IsNotNull(existing);
			Assert.AreEqual(newKnowledgebase.ID, existing.ID);
			Assert.AreEqual(newKnowledgebase.Name, existing.Name);
			Assert.AreEqual(newKnowledgebase.CreatedAt, existing.CreatedAt);
			Assert.AreEqual(newKnowledgebase.UpdatedAt, existing.UpdatedAt);

			await wrapper.Knowledgebase.DeleteKnowledgebase(newKnowledgebase.ID);
		}

		[TestMethod]
		public async Task Can_GetKnowledgebaseByName()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var newKnowledgebase = await wrapper.Knowledgebase.CreateKnowledgebase("getknowledgebasebyname");

			// ACT
			var existing = await wrapper.Knowledgebase.GetKnowledgebaseByName(newKnowledgebase.Name);

			// ASSERT
			Assert.IsNotNull(newKnowledgebase);
			Assert.IsNotNull(existing);
			Assert.AreEqual(newKnowledgebase.ID, existing.ID);
			Assert.AreEqual(newKnowledgebase.Name, existing.Name);
			Assert.AreEqual(newKnowledgebase.CreatedAt, existing.CreatedAt);
			Assert.AreEqual(newKnowledgebase.UpdatedAt, existing.UpdatedAt);

			await wrapper.Knowledgebase.DeleteKnowledgebase(newKnowledgebase.ID);
		}

		[TestMethod]
		public async Task Can_AddFilesToKnowledgebase()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var newKnowledgebase = await wrapper.Knowledgebase.CreateKnowledgebase("addfilesknowledgebase");
			var existing = await wrapper.Knowledgebase.GetKnowledgebaseByID(newKnowledgebase.ID);
			Assert.IsTrue(existing.Files.Count == 0);

			// ACT
			await wrapper.Knowledgebase.AddFileToKnowledgebase("add file test", newKnowledgebase.ID, "add.txt");

			// ASSERT
			existing = await wrapper.Knowledgebase.GetKnowledgebaseByID(newKnowledgebase.ID);
			Assert.IsTrue(existing.Files.Count == 1);
			Assert.AreEqual("add.txt", existing.Files[0].Name);

			await wrapper.Knowledgebase.RemoveFileFromCollection(existing.Files[0].ID, existing.ID);
			await wrapper.Knowledgebase.DeleteKnowledgebase(newKnowledgebase.ID);
		}

		[TestMethod]
		public async Task Can_RemoveFilesToKnowledgebase()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var newKnowledgebase = await wrapper.Knowledgebase.CreateKnowledgebase("removefilesknowledgebase");
			var existing = await wrapper.Knowledgebase.GetKnowledgebaseByID(newKnowledgebase.ID);
			Assert.IsTrue(existing.Files.Count == 0);
			await wrapper.Knowledgebase.AddFileToKnowledgebase("delete file test", newKnowledgebase.ID, "delete.txt");
			existing = await wrapper.Knowledgebase.GetKnowledgebaseByID(newKnowledgebase.ID);
			Assert.IsTrue(existing.Files.Count == 1);

			// ACT
			await wrapper.Knowledgebase.RemoveFileFromCollection(existing.Files[0].ID, existing.ID);

			// ASSERT
			existing = await wrapper.Knowledgebase.GetKnowledgebaseByID(newKnowledgebase.ID);
			Assert.IsTrue(existing.Files.Count == 0);

			await wrapper.Knowledgebase.DeleteKnowledgebase(newKnowledgebase.ID);
		}

		[ClassCleanup]
		public static async Task ClassCleanup()
		{
			await DeleteAllKnowledgebases();
		}

		private static async Task DeleteAllKnowledgebases()
		{
			var wrapper = new KnowledgebaseWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var knowledgebases = await wrapper.GetAllKnowledgebases();
			foreach (var knowledgebase in knowledgebases)
			{
				foreach (var file in knowledgebase.Files)
					await wrapper.RemoveFileFromCollection(file.ID, knowledgebase.ID);
				await wrapper.DeleteKnowledgebase(knowledgebase.ID);
			}
		}
	}
}
