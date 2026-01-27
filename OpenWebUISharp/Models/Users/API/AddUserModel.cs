using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Users.API
{
	public class AddUserModel
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("email")]
		public string Email { get; set; }
		[JsonPropertyName("password")]
		public string Password { get; set; }
		[JsonPropertyName("role")]
		public string Role { get; set; }
	}
}
