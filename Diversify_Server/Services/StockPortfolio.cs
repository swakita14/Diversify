using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Diversify_Server.Interfaces.Repositories;
using Diversify_Server.Interfaces.Services;
using Diversify_Server.Models.Database;
using Diversify_Server.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Diversify_Server.Services
{
    public class StockPortfolio : IStockPortfolioService
    {
        private readonly IStockRepository _stockRepository;
        private readonly ISectorRepository _sectorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StockPortfolio(IStockRepository stockRepository, ISectorRepository sectorRepository, IHttpContextAccessor httpContextAccessor)
        {
            _stockRepository = stockRepository;
            _sectorRepository = sectorRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        /**
         *  Retrieves all the stocks that the user has added 
         */
        public async Task<List<StockTransactionViewModel>> GetCurrentUserStockTransaction()
        {
            // Return all the stocks that the user has
            var userStocks =  await _stockRepository.GetStockByUserId(GetCurrentLoggedInUser());

            var transactionList = new List<StockTransactionViewModel>();

            foreach (var transactions in userStocks)
            {
                transactionList.Add(new StockTransactionViewModel
                {
                    CompanyName = transactions.Name,
                    DividendYield = Decimal.Parse(transactions.DividendYield)*100,
                    PurchaseDate = transactions.PurchaseDate,
                    Symbol = transactions.Symbol,
                    PurchasePrice = transactions.InvestmentAmount,
                    StockId = transactions.StockId,
                    Sector = _sectorRepository.GetSectorNameById(transactions.Sector)
                });

            }

            return transactionList;
        }

        public async Task<List<Stock>> GetCurrentUserStocks()
        {
            return await _stockRepository.GetStockByUserId(GetCurrentLoggedInUser());
        }

        /**
         * Retrieves User stock portfolio list
         */
        public async Task<IEnumerable<StockPortfolioViewModel>> StockPortfolioGroupByCompany()
        {
            // Get current user stocks 
            var currentStockList = await GetCurrentUserStocks();

            var groupedListBySymbol = currentStockList.GroupBy(x => x.Symbol)
                .Select(y => new StockPortfolioViewModel
                {
                    CompanyName = currentStockList.First(x => x.Symbol == y.Key).Name,
                    Symbol = y.Key,
                    DividendYield = decimal.Parse(currentStockList.First(x => x.Symbol == y.Key).DividendYield),
                    TotalInvestment = y.Sum(x => x.InvestmentAmount),
                    ExDividendDate = currentStockList.First(x => x.Symbol == y.Key).ExDividendDate,
                    Sector = _sectorRepository.GetSectorNameById(currentStockList.First(x => x.Symbol == y.Key).Sector)
                }).ToList();
            
            return  groupedListBySymbol;
        }            
        
        public async Task<IEnumerable<StockPortfolioViewModel>> StockPortfolioGroupBySector()
        {
            // Get current user stocks 
            var currentStockList = await GetCurrentUserStocks();

            var groupedListBySymbol = currentStockList.GroupBy(x => x.Sector)
                .Select(y => new StockPortfolioViewModel
                {
                    //DividendYield = decimal.Parse(currentStockList.First(x => x.Sector == y.Key).DividendYield),
                    TotalInvestment = y.Sum(x => x.InvestmentAmount),
                    Sector = _sectorRepository.GetSectorNameById(currentStockList.First(x => x.Sector == y.Key).Sector)
                }).ToList();
            
            return  groupedListBySymbol;
        }        
        

        /**
         * Gets the userId of the current logged in user
         */
        public string GetCurrentLoggedInUser()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
