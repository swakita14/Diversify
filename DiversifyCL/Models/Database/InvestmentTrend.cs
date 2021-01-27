using System;
using System.Collections.Generic;
using System.Text;

namespace DiversifyCL.Models.Database
{
    public class InvestmentTrend
    {
        public int  InvestmentTrendsId { get; set; }
        public DateTime DateModified { get; set; }
        public decimal InvestmentAmount { get; set; }
        public int  Company { get; set; }
        public string User { get; set; }
    }
}
