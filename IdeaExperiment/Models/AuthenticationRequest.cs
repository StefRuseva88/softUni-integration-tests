using System.Text.Json.Serialization;
namespace IdeaExperiment.Models
{
    public class AuthenticationRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
