using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diversify_Server.Models.ViewModels
{
    public class StockTransactionViewModel
    {
        public int  StockId { get; set; }
        public string CompanyName { get; set; }

        public decimal PurchasePrice { get; set; }
        public decimal DividendYield { get; set; }
        public string Symbol { get; set; }

        public string Sector { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime SoldDate { get; set; }

        public decimal SoldPrice { get; set; }
    }
}
