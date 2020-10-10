

namespace Diversify_Server.Models
{

    public class CompanyOverviewModel
        {
            public string Symbol { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Sector { get; set; }
            public string Industry { get; set; }
            public string DividendPerShare { get; set; }
            public string DividendYield { get; set; }
            public string PayoutRatio { get; set; }
            public string DividendDate { get; set; }
            public string ExDividendDate { get; set; }
        }

    
}
