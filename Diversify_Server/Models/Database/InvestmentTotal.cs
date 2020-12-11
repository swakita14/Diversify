using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diversify_Server.Models.Database
{
    public class InvestmentTotal
    {
        public int InvestmentTotalId { get; set; }

        public string Symbol { get; set; }
        
        public decimal InvestedAmount { get; set; }

        public string User { get; set; }
    }
}
