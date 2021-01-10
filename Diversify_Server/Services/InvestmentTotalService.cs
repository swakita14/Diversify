using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Diversify_Server.Interfaces.Repositories;
using Diversify_Server.Interfaces.Services;
using Diversify_Server.Models.Database;

namespace Diversify_Server.Services
{
    public class InvestmentTotalService : IInvestmentTotalService
    {
        private readonly IInvestmentTotalRepository _investmentTotalRepository;
        private readonly IIdentityService _identityService;

        public InvestmentTotalService(IInvestmentTotalRepository investmentTotalRepository, IIdentityService identityService)
        {
            _investmentTotalRepository = investmentTotalRepository;
            _identityService = identityService;
        }


        /**
         * Get the sum of user investment
         */
        public async Task<decimal> GetUserTotalInvestment()
        {
            var currentUserInvestmentTotals =
                await _investmentTotalRepository.GetInvestmentTotalByUserId(_identityService.GetCurrentLoggedInUser());

            return currentUserInvestmentTotals.Sum(x => x.InvestedAmount);
        }

        /**
         * If the edit investment amount is larger than the current investment, will return false, else true
         */
        public async Task<bool> CheckRemainderInvestment(string companySymbol, decimal editInvestmentAmount)
        {
            var currentInvestment = await _investmentTotalRepository.GetInvestedTotalByCompanySymbol(companySymbol, _identityService.GetCurrentLoggedInUser());

            if (currentInvestment < editInvestmentAmount)
            {
                return false;
            }

            return true;
        }

        public async Task<decimal> GetInvestmentTotalWithCompanySymbol(string symbol)
        {
            var existing =
                await _investmentTotalRepository.GetInvestedTotalByCompanySymbol(symbol,
                    _identityService.GetCurrentLoggedInUser());

            return existing;
        }

    }
}
