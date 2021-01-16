using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiversifyCL.Models.ViewModels;

namespace DiversifyCL.Interfaces.Services
{
    public interface IStockPortfolioService
    {
        /**
         *  Retrieves all the stocks that the user has added 
         */
        Task<List<StockTransactionViewModel>> GetCurrentUserStockTransaction();

        Task<IEnumerable<StockPortfolioViewModel>> StockPortfolioGroupByCompany();

        Task<IEnumerable<StockPortfolioViewModel>> StockPortfolioGroupBySector();

        Task<List<StockTransactionViewModel>> GetCurrentUserStockTransactionSold();

    }
}