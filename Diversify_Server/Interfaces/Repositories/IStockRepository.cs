using System.Collections.Generic;
using System.Threading.Tasks;
using Diversify_Server.Models.Database;

namespace Diversify_Server.Interfaces.Repositories
{
    public interface IStockRepository
    {
        void AddStock(Stock stock);
        void DeleteStock(Stock stock);
        void Edit(Stock restaurant);

        int GetCompanyCountBySectorId(int sectorId);

        decimal GetAverageDividendBySector(int sectorId); 

        Task<List<Stock>> GetStockPurchasedByUserId(string userId);

        Task<List<Stock>> GetCurrentStockByUserId(string userId);
        Task<List<Stock>> GetStockSoldByUserId(string userId);
    }
}