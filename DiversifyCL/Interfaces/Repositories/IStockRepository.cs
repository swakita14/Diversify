using System.Collections.Generic;
using System.Threading.Tasks;
using DiversifyCL.Models.Database;

namespace DiversifyCL.Interfaces.Repositories
{
    public interface IStockRepository
    {
        Task AddStock(Stock stock);
        Task SellStock(Stock currentStock);
        Task DeleteStock(Stock stock);
        Task Edit(Stock currentStock);
        Task<List<Stock>> GetCurrentStockByUserId(string userId);
        Task<List<Stock>> GetStockSoldByUserId(string userId);
        Task<Stock> GetStockByCompanySymbol(string symbol);
    }
}