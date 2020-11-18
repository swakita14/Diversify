using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diversify_Server.Data;
using Diversify_Server.Interfaces.Repositories;
using Diversify_Server.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Diversify_Server.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly DiversifyContext _context;

        public StockRepository(DiversifyContext context)
        {
            _context = context;
        }

        public void AddStock(Stock stock)
        {
            _context.Stock.Add(stock);
            _context.SaveChanges();
        }

        public void DeleteStock(Stock stock)
        {
            Stock existing = _context.Stock.Find(stock.StockId);

            if (existing == null) throw new ArgumentException($"Count not find the restaurant with ID {stock.StockId}");

            _context.Stock.Remove(stock);
            _context.SaveChanges();
        }

        public void Edit(Stock restaurant)
        {
            _context.Entry(restaurant).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<List<Stock>> GetStockBySector(int sectorId)
        {
            return await _context.Stock.Where(x => x.Sector == sectorId).ToListAsync();
        }
        
        public async Task<List<Stock>> GetStockByIndustry(int industryId)
        {
            return await _context.Stock.Where(x => x.Industry == industryId).ToListAsync();
        }

        public async Task<List<Stock>> GetStockByUserId(string userId)
        {
            return await _context.Stock.Where(x => x.User == userId).ToListAsync();
        }
    }
}
