using System.Text.Json.Serialization;
namespace IdeaExperiment.Models
{
    public class ApiResponseDTO
    {
        [JsonPropertyName("msg")]
        public string Message { get; set; }

        [JsonPropertyName("id")]
        public string IdeaId { get; set; }
    }
}
