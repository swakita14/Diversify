

using System.Text.Json.Serialization;

namespace Diversify_Server.Models
{
    public class CompanyOverviewModel
    {
        [JsonPropertyName("Symbol")] public string Symbol { get; set; }

        [JsonPropertyName("AssetType")] public string AssetType { get; set; }

        [JsonPropertyName("Name")] public string Name { get; set; }

        [JsonPropertyName("Description")] public string Description { get; set; }

        [JsonPropertyName("Exchange")] public string Exchange { get; set; }

        [JsonPropertyName("Currency")] public string Currency { get; set; }

        [JsonPropertyName("Country")] public string Country { get; set; }

        [JsonPropertyName("Sector")] public string Sector { get; set; }

        [JsonPropertyName("Industry")] public string Industry { get; set; }

        [JsonPropertyName("Address")] public string Address { get; set; }

        [JsonPropertyName("FullTimeEmployees")]
        public string FullTimeEmployees { get; set; }

        [JsonPropertyName("FiscalYearEnd")] public string FiscalYearEnd { get; set; }

        [JsonPropertyName("LatestQuarter")] public string LatestQuarter { get; set; }

        [JsonPropertyName("MarketCapitalization")]
        public string MarketCapitalization { get; set; }

        [JsonPropertyName("EBITDA")] public string EBITDA { get; set; }

        [JsonPropertyName("PERatio")] public string PERatio { get; set; }

        [JsonPropertyName("PEGRatio")] public string PEGRatio { get; set; }

        [JsonPropertyName("BookValue")] public string BookValue { get; set; }

        [JsonPropertyName("DividendPerShare")] public string DividendPerShare { get; set; }

        [JsonPropertyName("DividendYield")] public string DividendYield { get; set; }

        [JsonPropertyName("EPS")] public string EPS { get; set; }

        [JsonPropertyName("RevenuePerShareTTM")]
        public string RevenuePerShareTTM { get; set; }

        [JsonPropertyName("ProfitMargin")] public string ProfitMargin { get; set; }

        [JsonPropertyName("OperatingMarginTTM")]
        public string OperatingMarginTTM { get; set; }

        [JsonPropertyName("ReturnOnAssetsTTM")]
        public string ReturnOnAssetsTTM { get; set; }

        [JsonPropertyName("ReturnOnEquityTTM")]
        public string ReturnOnEquityTTM { get; set; }

        [JsonPropertyName("RevenueTTM")] public string RevenueTTM { get; set; }

        [JsonPropertyName("GrossProfitTTM")] public string GrossProfitTTM { get; set; }

        [JsonPropertyName("DilutedEPSTTM")] public string DilutedEPSTTM { get; set; }

        [JsonPropertyName("QuarterlyEarningsGrowthYOY")]
        public string QuarterlyEarningsGrowthYOY { get; set; }

        [JsonPropertyName("QuarterlyRevenueGrowthYOY")]
        public string QuarterlyRevenueGrowthYOY { get; set; }

        [JsonPropertyName("AnalystTargetPrice")]
        public string AnalystTargetPrice { get; set; }

        [JsonPropertyName("TrailingPE")] public string TrailingPE { get; set; }

        [JsonPropertyName("ForwardPE")] public string ForwardPE { get; set; }

        [JsonPropertyName("PriceToSalesRatioTTM")]
        public string PriceToSalesRatioTTM { get; set; }

        [JsonPropertyName("PriceToBookRatio")] public string PriceToBookRatio { get; set; }

        [JsonPropertyName("EVToRevenue")] public string EVToRevenue { get; set; }

        [JsonPropertyName("EVToEBITDA")] public string EVToEBITDA { get; set; }

        [JsonPropertyName("Beta")] public string Beta { get; set; }

        [JsonPropertyName("52WeekHigh")] public string FiftyTwoWeekHigh { get; set; }

        [JsonPropertyName("52WeekLow")] public string FiftyTwoWeekLow { get; set; }

        [JsonPropertyName("50DayMovingAverage")]
        public string FiftyDayMovingAverage { get; set; }

        [JsonPropertyName("200DayMovingAverage")]
        public string TwoHundredDayMovingAverage { get; set; }

        [JsonPropertyName("SharesOutstanding")]
        public string SharesOutstanding { get; set; }

        [JsonPropertyName("SharesFloat")] public string SharesFloat { get; set; }

        [JsonPropertyName("SharesShort")] public string SharesShort { get; set; }

        [JsonPropertyName("SharesShortPriorMonth")]
        public string SharesShortPriorMonth { get; set; }

        [JsonPropertyName("ShortRatio")] public string ShortRatio { get; set; }

        [JsonPropertyName("ShortPercentOutstanding")]
        public string ShortPercentOutstanding { get; set; }

        [JsonPropertyName("ShortPercentFloat")]
        public string ShortPercentFloat { get; set; }

        [JsonPropertyName("PercentInsiders")] public string PercentInsiders { get; set; }

        [JsonPropertyName("PercentInstitutions")]
        public string PercentInstitutions { get; set; }

        [JsonPropertyName("ForwardAnnualDividendRate")]
        public string ForwardAnnualDividendRate { get; set; }

        [JsonPropertyName("ForwardAnnualDividendYield")]
        public string ForwardAnnualDividendYield { get; set; }

        [JsonPropertyName("PayoutRatio")] public string PayoutRatio { get; set; }

        [JsonPropertyName("DividendDate")] public string DividendDate { get; set; }

        [JsonPropertyName("ExDividendDate")] public string ExDividendDate { get; set; }

        [JsonPropertyName("LastSplitFactor")] public string LastSplitFactor { get; set; }

        [JsonPropertyName("LastSplitDate")] public string LastSplitDate { get; set; }
    }
}






