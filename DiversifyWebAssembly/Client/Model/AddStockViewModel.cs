using System;

namespace DiversifyWebAssembly.Client.Model
{
    public class AddStockViewModel
    {
        public string CompanyName { get; set; }
        public string CompanySymbol { get; set; }
        public string DividendYield { get; set; }
        public string ExDividendDate { get; set; }
        public string EPS { get; set; }
        public string Exchange { get; set; }
        public string PayoutRatio { get; set; }
        public string Sector { get; set; }
        public string PERatio {get;set;}
        public decimal InvestmentAmount { get; set; }
        public  DateTime PurchaseDateTime { get; set; }
    }

}