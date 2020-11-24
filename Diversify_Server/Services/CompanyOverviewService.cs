using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Diversify_Server.Interfaces.Services;
using Diversify_Server.Models;
using Microsoft.Extensions.Configuration;

namespace Diversify_Server.Services
{
    public class CompanyOverviewService : ICompanyOverviewService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public CompanyOverviewService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<CompanyOverviewModel> GetCompanyOverviewAsync(string symbol)
        {
            // Initialize new model
            CompanyOverviewModel overview = new CompanyOverviewModel();

            // Getting ApiKey from Config 
            var apiKey = _configuration["StockApi:ApiKey"];

            // Call API with stock symbol
            try
            {
                overview = await _client.GetFromJsonAsync<CompanyOverviewModel>(
                    $"query?function=OVERVIEW&symbol={symbol}&apikey={apiKey}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // Return results 
            return overview;
        }
    }
}
