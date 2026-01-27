using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.Users
{
	public class UserModel
	{
		[JsonPropertyName("id")]
		public Guid ID { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Role { get; set; }
		public string Token { get; set; }
	}
}
