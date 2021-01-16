using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiversifyCL.Models
{

    public class SearchModel
    {
        [JsonPropertyName("1. symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("2. name")]
        public string Name { get; set; }

        [JsonPropertyName("3. type")]
        public string Type
        { get; set; }

        [JsonPropertyName("4. region")]
        public string Region
        { get; set; }

        [JsonPropertyName("5. marketOpen")]
        public string MarketOpen
        { get; set; }

        [JsonPropertyName("6. marketClose")]
        public string MarketClose
        { get; set; }

        [JsonPropertyName("7. timezone")]
        public string Timezone
        { get; set; }

        [JsonPropertyName("8. currency")]
        public string Currency
        { get; set; }

        [JsonPropertyName("9. matchScore")]
        public string MatchScore
        {
            get;
            set;
        }
    }

    public class SearchModelList
    {
        [JsonPropertyName("bestMatches")]
        public List<SearchModel> BestMatches { get; set; }
    }



}
