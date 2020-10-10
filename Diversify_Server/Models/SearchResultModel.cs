using System.Text.Json.Serialization;

namespace Diversify_Server.Models
{
    public class SearchResultModel
    {
        [JsonPropertyName("1.symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("2.name")]
        public string Name { get; set; }

        [JsonPropertyName("3.type")]
        public string Type { get; set; }

        [JsonPropertyName("9.matchScore")]
        public string MatchScore { get; set; } 
    }


}
