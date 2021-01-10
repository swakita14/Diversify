using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diversify_Server.Models.Database
{
    public class Company
    {
        public int  CompanyId { get; set; }

        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Exchange { get; set; }
        public decimal? EPS { get; set; }
        public decimal DividendYield { get; set; }
        public DateTime ExDividendDate { get; set; }
        public decimal? ProfitMargin { get; set; }
        public decimal? PERatio { get; set; }
        public decimal? PayoutRatio { get; set; }
        public int Sector { get; set; }
    }
}
