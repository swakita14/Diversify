using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private readonly IInvestmentTotalService _investmentTotalService;

        public StockPortfolioService(IStockRepository stockRepository, ISectorRepository sectorRepository, IIdentityService identityService, IInvestmentTotalService investmentTotalService)
        {
            _stockRepository = stockRepository;
            _sectorRepository = sectorRepository;
            _identityService = identityService;
            _investmentTotalService = investmentTotalService;
        }

        /**
         *  Retrieves all the stocks that the user has added 
         */
        public async Task<List<StockTransactionViewModel>> GetCurrentUserStockTransaction()
        {
            // Return all the stocks that the user has
            var userStocks = await _stockRepository.GetStockPurchasedByUserId(_identityService.GetCurrentLoggedInUser());

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
            var userStocks = await _stockRepository.GetStockSoldByUserId(_identityService.GetCurrentLoggedInUser());

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
            var currentStockList = await _investmentTotalService.GetInvestmentTotalByUserId();

            var stockPortfolio = new List<StockPortfolioViewModel>();

            // loop through each of investment totals 
            foreach (var stock in currentStockList)
            {
                // find company information
                var existingStock = await _stockRepository.GetCompanyBySymbol(stock.Symbol);

                // create new object to pass back 
                stockPortfolio.Add(new StockPortfolioViewModel
                {
                    CompanyName = existingStock.Name,
                    Symbol = stock.Symbol,
                    DividendYield = existingStock.DividendYield,
                    TotalInvestment = await _investmentTotalService.GetInvestedTotalByCompanySymbol(stock.Symbol),
                    ExDividendDate = existingStock.ExDividendDate,
                    Sector = _sectorRepository.GetSectorNameById(stock.Sector),
                    InvestedPercentage = await _investmentTotalService.GetInvestedTotalByCompanySymbol(stock.Symbol) / await _investmentTotalService.GetUserTotalInvestment()
                });
            }

            return stockPortfolio;
        }

        /**
         * Getting the stocks and grouping it by sector
         */
        public async Task<IEnumerable<StockPortfolioViewModel>> StockPortfolioGroupBySector()
        {
            var userInvestments = await _investmentTotalService.GetInvestmentTotalByUserId();


            var groupedListBySymbol = userInvestments.GroupBy(x => x.Sector)
                .Select(y => new StockPortfolioViewModel
                {
                    TotalInvestment = y.Sum(x => x.InvestedAmount),
                    Sector = _sectorRepository.GetSectorNameById(userInvestments.First(x => x.Sector == y.Key).Sector),
                    AverageDividend = decimal.Round(((_stockRepository.GetTotalDividendBySector(y.Key) / _stockRepository.GetCompanyCountBySectorId(y.Key))), 4, MidpointRounding.AwayFromZero)
                }).ToList();

            return groupedListBySymbol;
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

            // Adding a new investment, if already existing, add to the total 
            if (!await _investmentTotalService.CheckExistingInvestment(model.Symbol))
            {
                await _investmentTotalService.AddNewInvestment(model.Symbol, investmentAmount, _sectorRepository.GetSectorIdByName(model.Sector).SectorId);
            }
            else
            {
                await _investmentTotalService.EditExistingInvestment(model.Symbol, investmentAmount);
            }


            await _stockRepository.AddStock(newStock);
        }

        /**
         * Sell the user owned stock by the company symbol and the amount 
         */
        public async Task SellStock(string symbol, decimal amount, DateTime dateSold)
        {
            // Make changes to the total amount 
            await _investmentTotalService.EditExistingInvestment(symbol, amount);

            // Find the stock information from the database 
            var stockInformation = await _stockRepository.GetCompanyBySymbol(symbol);

            // Add a new new stock with the transaction amount 
            var newStockTransaction = new Stock
            {
                Name = stockInformation.Name,
                Symbol = stockInformation.Symbol,
                DividendYield = stockInformation.DividendYield,
                ExDividendDate = stockInformation.ExDividendDate,
                Status = 2,
                User = _identityService.GetCurrentLoggedInUser(),
                InvestmentAmount = amount,
                SoldDate = dateSold,
                Sector = stockInformation.Sector
            };
            await _stockRepository.AddStock(stockInformation);
        }
    }
}
