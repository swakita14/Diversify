using System;
using System.Threading.Tasks;
using DiversifyCL.Models;
using DiversifyCL.Models.Database;
using DiversifyCL.Models.ViewModels;

namespace DiversifyCL.Interfaces.Services
{
    public interface IStockService
    {
        Task<SearchModelList> GetStockAsync(string keyword);

        Task AddStockAsync(CompanyOverviewModel model, decimal investmentAmount ,DateTime purchaseTime);

        Task SellStock(string symbol, decimal amount, DateTime dateSold);

        Task<Stock> GetStockWithCompanySymbol(string symbol);
    }
}