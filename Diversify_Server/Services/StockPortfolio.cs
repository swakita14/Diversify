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
            var userStocks =  await _stockRepository.GetStockPurchasedByUserId(GetCurrentLoggedInUser());

            var transactionList = new List<StockTransactionViewModel>();

            foreach (var transactions in userStocks)
            {
                transactionList.Add(new StockTransactionViewModel
                {
                    CompanyName = transactions.Name,
                    DividendYield = transactions.DividendYield,
                    PurchaseDate = transactions.PurchaseDate,
                    Symbol = transactions.Symbol,
                    PurchasePrice = transactions.InvestmentAmount,
                    StockId = transactions.StockId,
                    Sector = _sectorRepository.GetSectorNameById(transactions.Sector)
                });

            }

            return transactionList;
        }        
        
        /**
         * Retrieve stocks that have been sold 
         */
        public async Task<List<StockTransactionViewModel>> GetCurrentUserStockTransactionSold()
        {
            // Return all the stocks that the user has
            var userStocks =  await _stockRepository.GetStockSoldByUserId(GetCurrentLoggedInUser());

            var transactionList = new List<StockTransactionViewModel>();

            foreach (var transactions in userStocks)
            {
                transactionList.Add(new StockTransactionViewModel
                {
                    CompanyName = transactions.Name,
                    DividendYield = transactions.DividendYield,
                    PurchaseDate = transactions.PurchaseDate,
                    Symbol = transactions.Symbol,
                    PurchasePrice = transactions.InvestmentAmount,
                    StockId = transactions.StockId,
                    Sector = _sectorRepository.GetSectorNameById(transactions.Sector)
                });

            }

            return transactionList;
        }


        /**
         * Retrieves all the stocks that the user currently owns 
         */
        public async Task<List<Stock>> GetCurrentUserStocks()
        {
            return await _stockRepository.GetCurrentStockByUserId(GetCurrentLoggedInUser());
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
                    DividendYield = decimal.Round((currentStockList.First(x => x.Symbol == y.Key).DividendYield ), 4, MidpointRounding.AwayFromZero),
                    TotalInvestment = y.Sum(x => x.InvestmentAmount),
                    ExDividendDate = currentStockList.First(x => x.Symbol == y.Key).ExDividendDate,
                    Sector = _sectorRepository.GetSectorNameById(currentStockList.First(x => x.Symbol == y.Key).Sector),
                    InvestedPercentage = (y.Sum(x => x.InvestmentAmount)) / _stockRepository.GetTotalInvestedByUserId(GetCurrentLoggedInUser())
                }).ToList();
            
            return  groupedListBySymbol;
        }            
        
        /**
         * Getting the stocks and grouping it by sector
         */
        public async Task<IEnumerable<StockPortfolioViewModel>> StockPortfolioGroupBySector()
        {
            // Get current user stocks 
            var currentStockList = await GetCurrentUserStocks();

            var groupedListBySymbol = currentStockList.GroupBy(x => x.Sector)
                .Select(y => new StockPortfolioViewModel
                {
                    TotalInvestment = y.Sum(x => x.InvestmentAmount),
                    Sector = _sectorRepository.GetSectorNameById(currentStockList.First(x => x.Sector == y.Key).Sector),
                    AverageDividend = decimal.Round(((_stockRepository.GetTotalDividendBySector(y.Key) / _stockRepository.GetCompanyCountBySectorId(y.Key))), 4, MidpointRounding.AwayFromZero)
                }).ToList();
            
            return  groupedListBySymbol;
        }

        /**
         * Gets the total amount invested and the estimated yearly income 
         */
        public async Task<InvestmentTotalViewModel> GetUserInvestmentTotal()
        {
            var currentUserStock = await GetCurrentUserStocks();

            InvestmentTotalViewModel totalViewModel = new InvestmentTotalViewModel();

            foreach (var stock in currentUserStock)
            {
                totalViewModel.InvestmentTotal += stock.InvestmentAmount;
                totalViewModel.EstimatedYearlyIncome = (stock.DividendYield * stock.InvestmentAmount);
            }

            return totalViewModel;
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
