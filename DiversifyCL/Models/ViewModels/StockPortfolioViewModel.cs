using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace DiversifyCL.Models.ViewModels
{
    public class StockPortfolioViewModel
    {

        public string CompanyName { get; set; }

        public decimal TotalInvestment { get; set; }

        public decimal DividendYield { get; set; }

        public DateTime ExDividendDate { get; set; }

        public string  Symbol { get; set; }

        public string  Sector { get; set; }

        public decimal AverageDividend { get; set; }

        public decimal InvestedPercentage { get; set; }
    }
}
