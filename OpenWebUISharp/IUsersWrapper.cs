using OpenWebUISharp.Models.Users;

namespace OpenWebUISharp
{
	/// <summary>
	/// Interface for the OpenWebUI users API
	/// </summary>
	public interface IUsersWrapper
	{
		/// <summary>
		/// The JWT token you can find in the OpenWebUI settings
		/// </summary>
		public string Token { get; set; }
		/// <summary>
		/// The URL (or IP) to OpenWebUI
		/// </summary>
		public string APIURL { get; set; }

		/// <summary>
		/// Gets all the users in the system
		/// </summary>
		/// <returns></returns>
		public Task<List<UserModel>> GetAll();

		/// <summary>
		/// Add a new user to the system
		/// </summary>
		/// <param name="name"></param>
		/// <param name="email"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public Task<UserModel> Add(string name, string email, string password, string role);

		/// <summary>
		/// Delete a user from the system
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task Delete(Guid id);
	}
}
