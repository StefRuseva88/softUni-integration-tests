using System.Text.Json.Serialization;

namespace StorySpoilApiTests.Models
{
    internal class AuthenticationResponse
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
    }
}
