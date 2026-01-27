using OpenWebUISharp.Models.Users;
using OpenWebUISharp.Models.Users.API;
using SerializableHttps;
using SerializableHttps.AuthenticationMethods;

namespace OpenWebUISharp
{
	/// <summary>
	/// Interface for the OpenWebUI users API
	/// </summary>
	public class UsersWrapper : IUsersWrapper
	{
		/// <summary>
		/// The JWT token you can find in the OpenWebUI settings
		/// </summary>
		public string Token { get; set; }
		/// <summary>
		/// The URL (or IP) to OpenWebUI
		/// </summary>
		public string APIURL { get; set; }


		private readonly SerializableHttpsClient _client;

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="token"></param>
		/// <param name="apiUrl"></param>
		public UsersWrapper(string token, string apiUrl)
		{
			Token = token;
			APIURL = apiUrl;


			_client = new SerializableHttpsClient();
			_client.SetAuthentication(new JWTAuthenticationMethod(token));
		}

		/// <summary>
		/// Add a new user to the system
		/// </summary>
		/// <param name="name"></param>
		/// <param name="email"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public async Task<UserModel> Add(string name, string email, string password)
		{
			var result = await _client.PostAsync<Models.Users.API.AddUserModel, UserModel>(new Models.Users.API.AddUserModel()
			{
				Name = name,
				Email = email,
				Password = password,
				Role = "user"
			}, APIURL + "/api/v1/auths/add");
			return result;
		}

		/// <summary>
		/// Delete a user from the system
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task Delete(Guid id)
		{
			await _client.DeleteAsync<string>(APIURL + "/api/v1/users/" + id);
		}

		/// <summary>
		/// Gets all the users in the system
		/// </summary>
		/// <returns></returns>
		public async Task<List<UserModel>> GetAll()
		{
			var users = await _client.GetAsync<GetAllUsersOutput>(APIURL + "/api/v1/users/");
			return users.Users;
		}
	}
}
