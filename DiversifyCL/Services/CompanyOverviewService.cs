using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DiversifyCL.Interfaces.Services;
using DiversifyCL.Models.ViewModels;
using Microsoft.Extensions.Configuration;

namespace DiversifyCL.Services
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
