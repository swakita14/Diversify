using System;
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
        public async Task<List<InvestmentTotal>> GetAllInvestmentTotalsByUserId(string userId)
        {
            return await _context.InvestmentTotals.Where(x => x.User == userId).ToListAsync();
        }
    }
}
