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
         * Find the amount invested for a specific company using the company symbol
         */
        public async Task<decimal> GetInvestedTotalByCompanySymbol(string symbol)
        {
            // Get all company stocks by user Id 
            var currentUserStocks =
                await _investmentTotalRepository.GetAllInvestmentTotalsByUserId(_identityService.GetCurrentLoggedInUser());

            // Find how the total amount by company 
            var companyTotal = currentUserStocks.FirstOrDefault(x => x.Symbol == symbol);

            return companyTotal.InvestedAmount;
        }

        /**
         * Adds a new investment using the company name
         */
        public async Task AddNewInvestment(string companySymbol, decimal initialInvestment)
        {
            InvestmentTotal newInvestmentTotal = new InvestmentTotal
            {
                InvestedAmount = initialInvestment,
                Symbol = companySymbol,
                User = _identityService.GetCurrentLoggedInUser()
            };

            await _investmentTotalRepository.AddNewTotal(newInvestmentTotal);
        }

        /**
         * Edit Investment amount
         */
        public async Task EditExistingInvestment(string companyName, decimal editInvestmentAmount)
        {
            await _investmentTotalRepository.EditInvestmentAmount(companyName, _identityService.GetCurrentLoggedInUser(),
                editInvestmentAmount);
        }

        /**
         * checking if the investment already exists, if not creatinga new one. 
         */
        public async Task<bool> CheckExistingInvestment(string companySymbol)
        {
            var allInvestmentTotalsByUser = await _investmentTotalRepository.GetAllInvestmentTotalsByUserId(_identityService.GetCurrentLoggedInUser());

            if (allInvestmentTotalsByUser.FirstOrDefault(x => x.Symbol == companySymbol) is null)
            {
                return false;
            }

            return true; 
        }
    }
}
