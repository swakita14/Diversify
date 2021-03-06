﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiversifyCL.Interfaces.Repositories;
using DiversifyCL.Interfaces.Services;
using DiversifyCL.Repositories;

namespace DiversifyCL.Services
{
    public class InvestmentTotalService : IInvestmentTotalService
    {
        private readonly IInvestmentTrendRepository _investmentTrendRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IIdentityService _identityService;

        public InvestmentTotalService(IInvestmentTrendRepository investmentTrendRepository, IIdentityService identityService, ICompanyRepository companyRepository)
        {
            _investmentTrendRepository = investmentTrendRepository;
            _companyRepository = companyRepository;
            _identityService = identityService;
        }


        /**
         * Get the sum of user investment
         */
        public async Task<decimal> GetUserTotalInvestment()
        {
            var currentUserInvestmentTotals =
                await _investmentTrendRepository.GetAllInvestmentByUserId(_identityService.GetCurrentLoggedInUser());

            return currentUserInvestmentTotals.Sum(x => x.InvestmentAmount);
        }



        public async Task<decimal> GetInvestmentTotalWithCompanySymbol(string symbol)
        {
            var company = await _companyRepository.GetCompanyBySymbol(symbol);

            var existing =
                await _investmentTrendRepository.GetInvestedTotalByCompany(company.CompanyId,
                    _identityService.GetCurrentLoggedInUser());

            return existing;
        }

    }
}
