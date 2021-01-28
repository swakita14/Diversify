using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DiversifyCL.Interfaces.Repositories;
using DiversifyCL.Interfaces.Services;
using DiversifyCL.Models;
using DiversifyCL.Models.Database;
using DiversifyCL.Models.ViewModels;
using DiversifyCL.Repositories;
using Microsoft.Extensions.Configuration;

namespace DiversifyCL.Services
{
    public class StockService : IStockService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly IStockRepository _stockRepository;
        private readonly IInvestmentTrendRepository _investmentTotalRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISectorRepository _sectorRepository;
        private readonly IIdentityService _identityService;

        public StockService(HttpClient client, IConfiguration configuration, IStockRepository stockRepository, IInvestmentTrendRepository investmentTotalRepository, ICompanyRepository companyRepository, ISectorRepository sectorRepository, IIdentityService identityService)
        {
            _client = client;
            _configuration = configuration;
            _stockRepository = stockRepository;
            _investmentTotalRepository = investmentTotalRepository;
            _companyRepository = companyRepository;
            _sectorRepository = sectorRepository;
            _identityService = identityService;
        }

        /**
         * Add new stock from component
         */
        public async Task AddStockAsync(CompanyOverviewModel model, decimal investmentAmount, DateTime purchaseTime)
        {
            if (!await _companyRepository.CheckExistingCompany(model.Symbol))
            {
                await _companyRepository.AddNewCompanyFromViewModel(model,
                    _sectorRepository.GetSectorIdByName(model.Sector).SectorId);
            }

            var companyId = await _companyRepository.GetCompanyBySymbol(model.Symbol);

            Stock newStock = new Stock
            {
                Company = companyId.CompanyId,
                InvestmentAmount = investmentAmount,
                PurchaseDate = purchaseTime,
                SoldDate = new DateTime(),
                Status = 1,
                User = _identityService.GetCurrentLoggedInUser()
            };

            // Adding a new investment, if already existing, add to the total 
            await _investmentTotalRepository.AddNewInvestment(companyId.CompanyId, investmentAmount,_identityService.GetCurrentLoggedInUser());
            await _stockRepository.AddStock(newStock);
        }

        /**
         * Gets the stock matches async using the keyword input
         */
        public async Task<SearchModelList> GetStockAsync(string keyword)
        {
            // Initialize the model 
            SearchModelList resultList = new SearchModelList();

            // Call the API with the parameter
            try
            {
                // Parse the JSON to C# model
                resultList = await _client.GetFromJsonAsync<SearchModelList>(
                    $"query?function=SYMBOL_SEARCH&keywords={keyword}&apikey={_configuration["StockApi:ApiKey"]}");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            // return the object 
            return resultList;
        }
        
        /**
         * Sell (Add) new stock from component
         */
        public async Task<bool> SellStock(string symbol, decimal amount, DateTime dateSold)
        {
            var company = await  _companyRepository.GetCompanyBySymbol(symbol);

            if (await _investmentTotalRepository.CheckRemainderInvestment(company.CompanyId, amount,
                _identityService.GetCurrentLoggedInUser()) && amount > 0)
            {
                throw new Exception("Cannot deduct more than current asset");
            }

            // Make changes to the total amount
            await _investmentTotalRepository.AddNewInvestment(company.CompanyId, amount, _identityService.GetCurrentLoggedInUser());

            // Find the stock information from the database 
            var stockInformation = await _companyRepository.GetCompanyBySymbol(symbol);

            // Add a new new stock with the transaction amount 
            var newStockTransaction = new Stock
            {
                Company = stockInformation.CompanyId,
                InvestmentAmount = amount,
                SoldDate = dateSold,
                Status = 2,
                User = _identityService.GetCurrentLoggedInUser()
            };

            await _stockRepository.AddStock(newStockTransaction);

            // operation successful
            return true;
        }

        /**
         * Get Stock with the specific company symbol
         */
        public async Task<Stock> GetStockWithCompanySymbol(string symbol)
        {
            var existing = await _stockRepository.GetStockByCompanySymbol(symbol);

            return existing;
        }

    }
}
