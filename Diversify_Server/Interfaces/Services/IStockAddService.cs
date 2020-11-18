using System.Collections.Generic;
using System.Threading.Tasks;
using Diversify_Server.Models.Database;

namespace Diversify_Server.Interfaces.Services
{
    public interface IStockAddService
    {
        /**
         *  Retrieves all the stocks that the user has added 
         */
        Task<List<Stock>> GetCurrentUserStocks();

        string GetCurrentLoggedInUser();
    }
}