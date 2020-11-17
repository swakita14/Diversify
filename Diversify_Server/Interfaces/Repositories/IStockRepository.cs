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
        Task<List<Stock>> GetStockBySector(int sectorId);
        Task<List<Stock>> GetStockByIndustry(int industryId);
    }
}