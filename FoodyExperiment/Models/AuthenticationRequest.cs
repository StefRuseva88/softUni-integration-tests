using System.Text.Json.Serialization;

namespace FoodyExperiment.Models
{
    public class AuthenticationRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
