using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DiversifyCL.Data;
using DiversifyCL.Interfaces.Repositories;
using DiversifyCL.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace DiversifyCL.Repositories
{
    public class InvestmentTotalRepository : IInvestmentTotalRepository
    {
        private readonly DiversifyContext _context;

        public InvestmentTotalRepository(DiversifyContext context)
        {
            _context = context;
        }

        /**
         * Adding new investment total if not existing 
         */
        public async Task AddNewTotal(InvestmentTotal newInvestmentTotal)
        {
            await _context.InvestmentTotals.AddAsync(newInvestmentTotal);

            _context.Entry(newInvestmentTotal).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        /**
         * Edit existing investment total
         */
        public async Task EditInvestmentAmount(InvestmentTotal existingInvestmentTotal)
        {
            _context.Entry(existingInvestmentTotal).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /**
         * Get investment totals by user Id 
         */
        public async Task<List<InvestmentTotal>> GetAllInvestmentTotalsByUserId(string userId)
        {
            return await _context.InvestmentTotals.Where(x => x.User == userId).ToListAsync();
        }

        /**
        * checking if the investment already exists, if not creating a  new one. 
        */
        public async Task<bool> CheckExistingInvestment(string companySymbol, string userId)
        {
            var allInvestmentTotalsByUser = await GetAllInvestmentTotalsByUserId(userId);

            if (allInvestmentTotalsByUser.FirstOrDefault(x => x.Symbol == companySymbol) is null)
            {
                return false;
            }

            return true;
        }

        /**
        * Adds a new investment using the company name
        */
        public async Task AddNewInvestment(string companySymbol, decimal initialInvestment, int sectorId, string userId)
        {
            InvestmentTotal newInvestmentTotal = new InvestmentTotal
            {
                InvestedAmount = initialInvestment,
                Symbol = companySymbol,
                User = userId,
                Sector = sectorId
            };

            await AddNewTotal(newInvestmentTotal);
        }

        public async Task<List<InvestmentTotal>> GetInvestmentTotalByUserId(string userId)
        {
            return await GetAllInvestmentTotalsByUserId(userId);
        }

        /**
        * Find the amount invested for a specific company using the company symbol
         */
        public async Task<decimal> GetInvestedTotalByCompanySymbol(string symbol, string userId)
        {
            // Get all company stocks by user Id 
            var currentUserStocks =
                await GetInvestmentTotalByUserId(userId);

            // Find how the total amount by company 
            var companyTotal = currentUserStocks.FirstOrDefault(x => x.Symbol == symbol);

            return companyTotal.InvestedAmount;
        }

        /**
         * Edit Investment amount
        */
        public async Task EditExistingInvestment(string companyName, decimal editInvestmentAmount, string userId)
        {
            var existingUserStocks =
                await GetInvestmentTotalByUserId(userId);

            var existingCompanyStock = existingUserStocks.FirstOrDefault(x => x.Symbol == companyName);

            existingCompanyStock.InvestedAmount += editInvestmentAmount;

            await EditInvestmentAmount(existingCompanyStock);
        }


        public async Task<decimal> GetUserTotalInvestment(string userId)
        {
            var currentUserInvestmentTotals =
                await GetInvestmentTotalByUserId(userId);

            return currentUserInvestmentTotals.Sum(x => x.InvestedAmount);
        }

        /**
 * If the edit investment amount is larger than the current investment, will return false, else true
 */
        public async Task<bool> CheckRemainderInvestment(string companySymbol, decimal editInvestmentAmount, string userId)
        {
            var currentInvestment = await GetInvestedTotalByCompanySymbol(companySymbol, userId);

            if (currentInvestment < editInvestmentAmount)
            {
                return false;
            }

            return true;
        }
    }
}
