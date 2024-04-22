using System.Text.Json.Serialization;
namespace IdeaExperiment.Models
{
    public class IdeaDTO
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    
    }
}
