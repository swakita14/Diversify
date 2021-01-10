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
        private readonly IInvestmentTotalRepository _investmentTotalRepository;
        private readonly ICompanyRepository _companyRepository;

        public StockPortfolioService(IStockRepository stockRepository, ISectorRepository sectorRepository, IIdentityService identityService, IInvestmentTotalRepository investmentTotalRepository, ICompanyRepository companyRepository)
        {
            _stockRepository = stockRepository;
            _sectorRepository = sectorRepository;
            _identityService = identityService;
            _investmentTotalRepository = investmentTotalRepository;
            _companyRepository = companyRepository;
        }

        /**
         *  Retrieves all the stocks that the user has added 
         */
        public async Task<List<StockTransactionViewModel>> GetCurrentUserStockTransaction()
        {
            // Return all the stocks that the user has
            var userStocks = await _stockRepository.GetCurrentStockByUserId(_identityService.GetCurrentLoggedInUser());

            var transactionList = new List<StockTransactionViewModel>();

            foreach (var transactions in userStocks)
            {
                transactionList.Add(new StockTransactionViewModel
                {
                    CompanyName = _companyRepository.GetCompanyByCompanyId(transactions.Company).Name,
                    DividendYield = _companyRepository.GetCompanyByCompanyId(transactions.Company).DividendYield,
                    PurchaseDate = transactions.PurchaseDate,
                    Symbol = _companyRepository.GetCompanyByCompanyId(transactions.Company).Symbol,
                    PurchasePrice = transactions.InvestmentAmount,
                    StockId = transactions.StockId,
                    Sector = _sectorRepository.GetSectorNameById(_companyRepository.GetCompanyByCompanyId(transactions.Company).Sector)
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
                    CompanyName = _companyRepository.GetCompanyByCompanyId(transactions.Company).Name,
                    DividendYield = _companyRepository.GetCompanyByCompanyId(transactions.Company).DividendYield,
                    SoldDate = transactions.SoldDate,
                    Symbol = _companyRepository.GetCompanyByCompanyId(transactions.Company).Symbol,
                    SoldPrice = transactions.InvestmentAmount,
                    StockId = transactions.StockId,
                    Sector = _sectorRepository.GetSectorNameById(_companyRepository.GetCompanyByCompanyId(transactions.Company).Sector)
                });

            }

            return transactionList;
        }


        /**
         * Retrieves User stock portfolio list
         */
        public async Task<IEnumerable<StockPortfolioViewModel>> StockPortfolioGroupByCompany()
        {
            // Get current user stocks 
            var currentStockList =
                await _investmentTotalRepository.GetInvestmentTotalByUserId(_identityService.GetCurrentLoggedInUser());

            var stockPortfolio = new List<StockPortfolioViewModel>();

            // loop through each of investment totals 
            foreach (var stock in currentStockList)
            {
                var company = await _companyRepository.GetCompanyBySymbol(stock.Symbol);

                // create new object to pass back 
                stockPortfolio.Add(new StockPortfolioViewModel
                {
                    CompanyName = company.Name,
                    Symbol = stock.Symbol,
                    DividendYield = company.DividendYield,
                    TotalInvestment = await _investmentTotalRepository.GetInvestedTotalByCompanySymbol(stock.Symbol, _identityService.GetCurrentLoggedInUser()),
                    ExDividendDate = company.ExDividendDate,
                    Sector = _sectorRepository.GetSectorNameById(stock.Sector),
                    InvestedPercentage = await _investmentTotalRepository.GetInvestedTotalByCompanySymbol(stock.Symbol, _identityService.GetCurrentLoggedInUser()) / await _investmentTotalRepository.GetUserTotalInvestment(_identityService.GetCurrentLoggedInUser())
                });
            }

            return stockPortfolio;
        }

        /**
         * Getting the stocks and grouping it by sector
         */
        public async Task<IEnumerable<StockPortfolioViewModel>> StockPortfolioGroupBySector()
        {
            var userInvestments = await _investmentTotalRepository.GetInvestmentTotalByUserId(_identityService.GetCurrentLoggedInUser());

            var groupedListBySymbol = userInvestments.GroupBy(x => x.Sector)
                .Select(y => new StockPortfolioViewModel
                {
                    TotalInvestment = y.Sum(x => x.InvestedAmount),
                    Sector = _sectorRepository.GetSectorNameById(userInvestments.First(x => x.Sector == y.Key).Sector),
                    AverageDividend = decimal.Round((( _companyRepository.GetTotalDividendBySector(y.Key) / _companyRepository.GetCompanyCountBySectorId(y.Key))), 4, MidpointRounding.AwayFromZero)
                }).ToList();

            return groupedListBySymbol;
        }
    }
}
