﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diversify_Server.Data;
using Diversify_Server.Interfaces.Repositories;
using Diversify_Server.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Diversify_Server.Repositories
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
        public async Task EditInvestmentAmount(InvestmentTotal investmentTotal)
        {
            var existing = await _context.InvestmentTotals.FindAsync(investmentTotal.InvestmentTotalId);

            _context.Entry(existing).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<List<InvestmentTotal>> GetAllInvestmentTotalsByUserId(string userId)
        {
            return await _context.InvestmentTotals.Where(x => x.User == userId).ToListAsync();
        }
    }
}
