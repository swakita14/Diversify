using DiversifyHangFire.Interface;
using Microsoft.Extensions.Configuration;
using System;

namespace DiversifyHangFire.Services
{
    public class OverviewUpdateService : IOverviewUpdateService
    {
        private readonly IConfiguration _configuration;

        public OverviewUpdateService()
        {

        }
        public void UpdateCompanyOverview()
        {
            Console.WriteLine("This task has been executed");
        }
    }
}
