using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiversifyCL.Models.Database
{
    public class Stock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StockId { get; set; }
        public string User { get; set; }

        public int  Company { get; set; }

        public int  Status { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime SoldDate { get; set; }

        [Required]
        public decimal InvestmentAmount { get; set; }
    }
}
