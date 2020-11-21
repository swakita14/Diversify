using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Diversify_Server.Interfaces.Repositories;
using Diversify_Server.Interfaces.Services;
using Diversify_Server.Models.Database;
using Microsoft.AspNetCore.Http;

namespace Diversify_Server.Services
{
    public class StockPortfolio : IStockPortfolioService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StockPortfolio(IStockRepository stockRepository, IHttpContextAccessor httpContextAccessor)
        {
            _stockRepository = stockRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        /**
         *  Retrieves all the stocks that the user has added 
         */
        public async Task<List<Stock>> GetCurrentUserStockTransaction()
        {
            // Retrieve current logged-in user id
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // Return all the stocks that the user has
            return await _stockRepository.GetStockByUserId(userId);
        }

        /**
         * Retrieves User stock portfolio list
         */
        public async Task<List<Stock>> GetCurrentUserStockPortfolio()
        {
            return await null;
        }

        public string GetCurrentLoggedInUser()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
