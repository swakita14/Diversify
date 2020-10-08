using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Diversify_Server.Models
{
    public class Stock
    {
        public int StockId { get; set; }

        [JsonPropertyName("Symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("Sector")]
        public string Sector { get; set; }

        [JsonPropertyName("Industry")]
        public string Industry { get; set; }

        [JsonPropertyName("DividendYield")]
        public string DividendYield { get; set; }
        [JsonPropertyName("DividendDate")]
        public string DividendDate { get; set; }

    }
}
