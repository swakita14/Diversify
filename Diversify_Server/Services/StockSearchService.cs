using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Diversify_Server.Interfaces;
using Diversify_Server.Models;
using Microsoft.Extensions.Configuration;

namespace Diversify_Server.Services
{
    public class StockSearchService : IStockSearchService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public StockSearchService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        /**
         * Gets the stock matches async using the keyword input
         */
        public async Task<SearchModelList> GetStockAsync(string keyword)
        {
            SearchModelList resultList = new SearchModelList();
            string _errorMessage = "";

            // Call the API with the parameter
            try
            {
                resultList = await _client.GetFromJsonAsync<SearchModelList>($"query?function=SYMBOL_SEARCH&keywords={keyword}&apikey={_configuration["StockApi:ApiKey"]}");
                _errorMessage = null;

            }
            catch (Exception ex)
            {
                _errorMessage = $"There was an error getting the company overview data: {ex.Message}";
            }

            return resultList;
        }
    }
}
