namespace OpenWebUISharp.Tests
{
	[TestClass]
	public class UsersTests
	{
		[ClassInitialize]
		public static async Task ClassInit(TestContext context)
		{
			await DeleteAllUsers();
		}

		[TestMethod]
		public async Task Can_AddUser()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var users = await wrapper.Users.GetAll();
			Assert.IsFalse(users.Any(x => x.Name == "testuser"));

			// ACT
			var result = await wrapper.Users.Add("testuser", "test@user.com", "password");

			// ASSERT
			Assert.IsNotNull(result);
			Assert.AreEqual("testuser", result.Name);
			users = await wrapper.Users.GetAll();
			Assert.IsTrue(users.Any(x => x.Name == "testuser"));

			await wrapper.Users.Delete(result.ID);
		}

		[TestMethod]
		public async Task Can_DeleteUser()
		{
			// ARRANGE
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var users = await wrapper.Users.GetAll();
			Assert.IsFalse(users.Any(x => x.Name == "testuser"));
			var user = await wrapper.Users.Add("testuser", "test@user.com", "password");
			users = await wrapper.Users.GetAll();
			Assert.IsTrue(users.Any(x => x.Name == "testuser"));

			// ACT
			await wrapper.Users.Delete(user.ID);

			// ASSERT
			users = await wrapper.Users.GetAll();
			Assert.IsFalse(users.Any(x => x.Name == "testuser"));
		}

		[ClassCleanup]
		public static async Task ClassCleanup()
		{
			await DeleteAllUsers();
		}

		private static async Task DeleteAllUsers()
		{
			var wrapper = new UsersWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var users = await wrapper.GetAll();
			foreach (var user in users)
				if (user.Name != "admin")
					await wrapper.Delete(user.ID);
		}
	}
}
