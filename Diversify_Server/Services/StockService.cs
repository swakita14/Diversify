using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Diversify_Server.Interfaces.Repositories;
using Diversify_Server.Interfaces.Services;
using Diversify_Server.Models;
using Diversify_Server.Models.Database;
using Microsoft.Extensions.Configuration;

namespace Diversify_Server.Services
{
    public class StockService : IStockService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly IStockRepository _stockRepository;
        private readonly IInvestmentTotalRepository _investmentTotalRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISectorRepository _sectorRepository;
        private readonly IIdentityService _identityService;

        public StockService(HttpClient client, IConfiguration configuration, IStockRepository stockRepository, IInvestmentTotalRepository investmentTotalRepository, ICompanyRepository companyRepository, ISectorRepository sectorRepository, IIdentityService identityService)
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
            if (!await _investmentTotalRepository.CheckExistingInvestment(model.Symbol, _identityService.GetCurrentLoggedInUser()))
            {
                await _investmentTotalRepository.AddNewInvestment(model.Symbol, investmentAmount, _sectorRepository.GetSectorIdByName(model.Sector).SectorId, _identityService.GetCurrentLoggedInUser());
            }
            else
            {
                await _investmentTotalRepository.EditExistingInvestment(model.Symbol, investmentAmount, _identityService.GetCurrentLoggedInUser());
            }

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
        public async Task SellStock(string symbol, decimal amount, DateTime dateSold)
        {
            // Make changes to the total amount 
            await _investmentTotalRepository.EditExistingInvestment(symbol, amount, _identityService.GetCurrentLoggedInUser());

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
