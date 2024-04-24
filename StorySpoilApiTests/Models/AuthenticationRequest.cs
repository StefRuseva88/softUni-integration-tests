using System.Text.Json.Serialization;

namespace StorySpoilApiTests.Models
{
    internal class AuthenticationRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
