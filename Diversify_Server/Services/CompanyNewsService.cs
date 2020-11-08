using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Diversify_Server.Interfaces;
using Diversify_Server.Models;
using Microsoft.Extensions.Configuration;

namespace Diversify_Server.Services
{
    public class CompanyNewsService : ICompanyNewsService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public CompanyNewsService(IConfiguration configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        /**
         * Async Task that will retrieve the list of news with the keyword
         */
        public async Task<NewsSearchResult> GetCompanyNewsAsync(string keyword)
        {
            // Initialize object 
            NewsSearchResult resultList = new NewsSearchResult();

            // Call the API with the parameter
            try
            {
                // Debugging, only allowed me to do it this way
                string apiKey = _configuration["NewsApi:ApiKey"];

                // Parse the JSON response into model 
                resultList = await _client.GetFromJsonAsync<NewsSearchResult>($"everything?domains=marketwatch.com&q={keyword}&apiKey={apiKey}");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            // Return the object 
            return resultList;
        }
    }


}
