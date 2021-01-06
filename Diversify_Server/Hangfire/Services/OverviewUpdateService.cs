
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using Diversify_Server.HangFire.Interfaces;
using Diversify_Server.Interfaces.Repositories;

namespace Diversify_Server.HangFire.Services 
{
    public class OverviewUpdateService : IOverviewUpdateService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IStockRepository _stockRepository;
        public OverviewUpdateService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IStockRepository stockRepository)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _stockRepository = stockRepository;
        }

        public void UpdateCompanyOverview()
        {
            Console.WriteLine("This task has been executed");
        }
    }
}
