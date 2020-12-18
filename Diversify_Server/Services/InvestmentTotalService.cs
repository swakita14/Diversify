﻿using System;
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
                await GetInvestmentTotalByUserId();

            // Find how the total amount by company 
            var companyTotal = currentUserStocks.FirstOrDefault(x => x.Symbol == symbol);

            return companyTotal.InvestedAmount;
        }

        public async Task<List<InvestmentTotal>> GetInvestmentTotalByUserId()
        {
            return await _investmentTotalRepository.GetAllInvestmentTotalsByUserId(_identityService
                .GetCurrentLoggedInUser());
        }

        /**
         * Gets the invested total with the sector Id
         */
        public async Task<IEnumerable<InvestmentTotal>> GetInvestedTotalBySector(int sectorId)
        {
            var currentUserInvestments =
                await GetInvestmentTotalByUserId();

            var investmentTotalBySector = currentUserInvestments.Where(x => x.Sector == sectorId);

            return investmentTotalBySector;

        }


        /**
         * Adds a new investment using the company name
         */
        public async Task AddNewInvestment(string companySymbol, decimal initialInvestment, int sectorId)
        {
            InvestmentTotal newInvestmentTotal = new InvestmentTotal
            {
                InvestedAmount = initialInvestment,
                Symbol = companySymbol,
                User = _identityService.GetCurrentLoggedInUser(),
                Sector = sectorId
            };

            await _investmentTotalRepository.AddNewTotal(newInvestmentTotal);
        }

        /**
         * Get the sum of user investment
         */
        public async Task<decimal> GetUserTotalInvestment()
        {
            var currentUserInvestmentTotals =
                await GetInvestmentTotalByUserId();

            return currentUserInvestmentTotals.Sum(x => x.InvestedAmount);
        }

        /**
         * Edit Investment amount
         */
        public async Task EditExistingInvestment(string companyName, decimal editInvestmentAmount)
        {
            var existingUserStocks =
                await GetInvestmentTotalByUserId();

            var existingCompanyStock = existingUserStocks.FirstOrDefault(x => x.Symbol == companyName);

            existingCompanyStock.InvestedAmount -= editInvestmentAmount;

            await _investmentTotalRepository.EditInvestmentAmount(existingCompanyStock);
        }

        /**
         * checking if the investment already exists, if not creating a  new one. 
         */
        public async Task<bool> CheckExistingInvestment(string companySymbol)
        {
            var allInvestmentTotalsByUser = await GetInvestmentTotalByUserId();

            if (allInvestmentTotalsByUser.FirstOrDefault(x => x.Symbol == companySymbol) is null)
            {
                return false;
            }

            return true; 
        }

        /**
         * If the edit investment amount is larger than the current investment, will return false, else true
         */
        public async Task<bool> CheckRemainderInvestment(string companySymbol, decimal editInvestmentAmount)
        {
            var currentInvestment = await GetInvestedTotalByCompanySymbol(companySymbol);

            if (currentInvestment < editInvestmentAmount)
            {
                return false;
            }

            return true;
        }
    }
}
