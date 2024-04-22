using System.Text.Json.Serialization;
namespace FoodyExperiment.Models
{
    public class ApiResponseDto
    {
        [JsonPropertyName("msg")]
        public string Message { get; set; }

        [JsonPropertyName("foodId")]
        public string FoodId { get; set; }
    }
}
