using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Diversify_Server.Models.Database
{
    public class Stock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StockId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Exchange { get; set; }
        public decimal? EPS { get; set; }
        public string DividendYield { get; set; }
        public DateTime ExDividendDate { get; set; }
        public decimal? ProfitMargin { get; set; }
        public decimal? PERatio { get; set; }
        public decimal? PayoutRatio { get; set; }
        public int Sector { get; set; }
        public string User { get; set; }

        public DateTime PurchaseDate { get; set; }

        [Required]
        public decimal InvestmentAmount { get; set; }
    }
}
