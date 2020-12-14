
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diversify_Server.Models.Database
{
    public class InvestmentTotal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvestmentTotalId { get; set; }

        public string Symbol { get; set; }
        
        public decimal InvestedAmount { get; set; }

        public string User { get; set; }

        public int  Sector { get; set; }
    }
}
