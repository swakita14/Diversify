using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Diversify_Server.Interfaces;
using Diversify_Server.Interfaces.Services;
using Diversify_Server.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;

namespace Diversify_Server.Services
{
    public class StockService : IStockSearchService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public StockService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
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
    }
}
