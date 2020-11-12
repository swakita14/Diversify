using System.Collections.Generic;
using Diversify_Server.Models.Database;

namespace Diversify_Server.Interfaces.Repositories
{
    public interface IStockRepository
    {
        Stock AddStock(Stock stock);
        void DeleteStock(Stock stock);
        void Edit(Stock restaurant);
        List<Stock> GetStockBySector(int sectorId);
        List<Stock> GetStockByIndustry(int industryId);
    }
}