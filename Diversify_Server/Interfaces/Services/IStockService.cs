using System;
using System.Threading.Tasks;
using Diversify_Server.Models;

namespace Diversify_Server.Interfaces.Services
{
    public interface IStockService
    {
        Task<SearchModelList> GetStockAsync(string keyword);

        Task AddStockAsync(CompanyOverviewModel model, decimal investmentAmount ,DateTime purchaseTime);

        Task SellStock(string symbol, decimal amount, DateTime dateSold);
    }
}