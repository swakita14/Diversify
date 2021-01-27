using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiversifyCL.Data;
using DiversifyCL.Interfaces.Repositories;
using DiversifyCL.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace DiversifyCL.Repositories
{
    public class InvestmentTrendRepository : IInvestmentTrendRepository 
    {
        private readonly DiversifyContext _context;
        public InvestmentTrendRepository(DiversifyContext context)
        {
            _context = context;
        }

        public async Task AddNewInvestment(int companyId, decimal initialInvestment, int sectorId, string userId)
        {
            InvestmentTrend newInvestment = new InvestmentTrend
            {
                Company = companyId,
                DateModified = DateTime.Now,
                InvestmentAmount = initialInvestment,
                User = userId
            };

            await AddNewTotal(newInvestment);
        }

        public async Task AddNewTotal(InvestmentTrend newInvestmentTotal)
        {
            await _context.InvestmentTrends.AddAsync(newInvestmentTotal);

            _context.Entry(newInvestmentTotal).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckExistingInvestment(int companyId, string userId)
        {
            var investmentsByUser = await GetAllInvestmentByUserId(userId);

            if (investmentsByUser.FirstOrDefault(x => x.Company == companyId) is null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CheckRemainderInvestment(int companyId, decimal editInvestmentAmount, string userId)
        {
            var currentInvestment = await GetInvestedTotalByCompany(companyId, userId);

            if (currentInvestment < editInvestmentAmount)
            {
                return false;
            }
            return true;

        }

        public async Task EditInvestmentAmount(InvestmentTrend existingInvestmentTotal)
        {
            _context.Entry(existingInvestmentTotal).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetInvestedTotalByCompany(int companyId, string userId)
        {
            var currentUserInvestments = await GetAllInvestmentByUserId(userId);

            return currentUserInvestments.FirstOrDefault(x => x.Company == companyId).InvestmentAmount;
        }

        public async Task<List<InvestmentTrend>> GetAllInvestmentByUserId(string userId)
        {
            return await _context.InvestmentTrends.Where(x => x.User == userId).ToListAsync();
        }

        public async Task<decimal> GetUserTotalInvestment(string userId)
        {
            var currentUserInvestments = await GetAllInvestmentByUserId(userId);

            return currentUserInvestments.Sum(x => x.InvestmentAmount);
        }
    }
}
