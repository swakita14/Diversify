using System.Collections.Generic;
using System.Threading.Tasks;
using Diversify_Server.Models.Database;

namespace Diversify_Server.Interfaces.Repositories
{
    public interface IStockRepository
    {
        Task AddStock(Stock stock);
        Task SellStock(Stock currentStock);
        Task DeleteStock(Stock stock);
        Task Edit(Stock restaurant);

        int GetCompanyCountBySectorId(int sectorId);

        decimal GetTotalDividendBySector(int sectorId);

        decimal GetTotalInvestedByUserId(string userId);

        int GetTotalCountStockByUserId(string userId);

        Task<List<Stock>> GetStockPurchasedByUserId(string userId);

        Task<List<Stock>> GetCurrentStockByUserId(string userId);
        Task<List<Stock>> GetStockSoldByUserId(string userId);
    }
}