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
        Task Edit(Stock currentStock);

        Task<Stock> GetCompanyBySymbol(string companySymbol);

        int GetCompanyCountBySectorId(int sectorId);

        decimal GetTotalDividendBySector(int sectorId);

        Task<List<Stock>> GetCurrentStockByUserId(string userId);
        Task<List<Stock>> GetStockSoldByUserId(string userId);
    }
}