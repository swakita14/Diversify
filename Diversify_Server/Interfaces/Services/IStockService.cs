using System;
using System.Threading.Tasks;
using Diversify_Server.Models;
using DiversifyCL.Models;
using DiversifyCL.Models.Database;

namespace Diversify_Server.Interfaces.Services
{
    public interface IStockService
    {
        Task<SearchModelList> GetStockAsync(string keyword);

        Task AddStockAsync(CompanyOverviewModel model, decimal investmentAmount ,DateTime purchaseTime);

        Task SellStock(string symbol, decimal amount, DateTime dateSold);

        Task<Stock> GetStockWithCompanySymbol(string symbol);
    }
}