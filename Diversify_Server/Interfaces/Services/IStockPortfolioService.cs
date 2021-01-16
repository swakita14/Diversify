using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Diversify_Server.Models.ViewModels;

namespace Diversify_Server.Interfaces.Services
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