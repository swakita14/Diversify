using System.Collections.Generic;
using System.Threading.Tasks;
using Diversify_Server.Models.Database;

namespace Diversify_Server.Interfaces.Services
{
    public interface IStockPortfolioService
    {
        /**
         *  Retrieves all the stocks that the user has added 
         */
        Task<List<Stock>> GetCurrentUserStockTransaction();

        Task<List<Stock>> GetCurrentUserStockPortfolio();

        string GetCurrentLoggedInUser();
    }
}