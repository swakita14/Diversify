using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiversifyCL.Interfaces.Repositories;
using DiversifyCL.Interfaces.Services;

namespace DiversifyCL.Services
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



        public async Task<decimal> GetInvestmentTotalWithCompanySymbol(string symbol)
        {
            var existing =
                await _investmentTotalRepository.GetInvestedTotalByCompanySymbol(symbol,
                    _identityService.GetCurrentLoggedInUser());

            return existing;
        }

    }
}
