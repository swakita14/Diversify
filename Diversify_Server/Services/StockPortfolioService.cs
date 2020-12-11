using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diversify_Server.Interfaces.Repositories;
using Diversify_Server.Interfaces.Services;
using Diversify_Server.Models;
using Diversify_Server.Models.Database;
using Diversify_Server.Models.ViewModels;

namespace Diversify_Server.Services
{
    public class StockPortfolioService : IStockPortfolioService
    {
        private readonly IStockRepository _stockRepository;
        private readonly ISectorRepository _sectorRepository;
        private readonly IIdentityService _identityService;

        public StockPortfolioService(IStockRepository stockRepository, ISectorRepository sectorRepository, IIdentityService identityService)
        {
            _stockRepository = stockRepository;
            _sectorRepository = sectorRepository;
            _identityService = identityService;
        }

        /**
         *  Retrieves all the stocks that the user has added 
         */
        public async Task<List<StockTransactionViewModel>> GetCurrentUserStockTransaction()
        {
            // Return all the stocks that the user has
            var userStocks =  await _stockRepository.GetStockPurchasedByUserId(_identityService.GetCurrentLoggedInUser());
            
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
            var userStocks =  await _stockRepository.GetStockSoldByUserId(_identityService.GetCurrentLoggedInUser());

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
            return await _stockRepository.GetCurrentStockByUserId(_identityService.GetCurrentLoggedInUser());
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
                    InvestedPercentage = (y.Sum(x => x.InvestmentAmount)) / _stockRepository.GetTotalInvestedByUserId(_identityService.GetCurrentLoggedInUser())
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
         * Adds new stock with the model, amount, and the time that the stock was purchased 
         */
        public async Task AddStock(CompanyOverviewModel model, decimal investmentAmount, DateTime purchaseDateTime)
        {
            Stock newStock = new Stock()
            {
                Name = model.Name,
                Symbol = model.Symbol,
                DividendYield = decimal.Parse(model.DividendYield),
                Sector = _sectorRepository.GetSectorIdByName(model.Sector).SectorId,
                InvestmentAmount = investmentAmount,
                User = _identityService.GetCurrentLoggedInUser(),
                Exchange = model.Exchange,
                EPS = Convert.ToDecimal(model.EPS),
                ExDividendDate = Convert.ToDateTime(model.ExDividendDate),
                PayoutRatio = Convert.ToDecimal(model.PayoutRatio),
                PERatio = Convert.ToDecimal(model.PERatio),
                ProfitMargin = Convert.ToDecimal(model.ProfitMargin),
                PurchaseDate = DateTime.Now.Date,
                Status = 1
            };

            await _stockRepository.AddStock(newStock);
        }

        public async Task SellStock(string symbol, decimal amount)
        {
            var existing = await _stockRepository.GetCompanyBySymbol(symbol);

            // Need to get company as a total first then subtract from it. 


            // Adding a new stock as a sold one instead of editing. 


        }
    }
}
