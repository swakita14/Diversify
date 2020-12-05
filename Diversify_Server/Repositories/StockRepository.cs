using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Diversify_Server.Data;
using Diversify_Server.Interfaces.Repositories;
using Diversify_Server.Models.Database;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.Schedule.Internal;

namespace Diversify_Server.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly DiversifyContext _context;

        public StockRepository(DiversifyContext context)
        {
            _context = context;
        }

        /**
         *  Adding Stock
         */
        public async Task AddStock(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        /**
         * Method to sell stocks 
         */
        public async Task SellStock(Stock currentStock)
        {
            var existing = await _context.Stock.FindAsync(currentStock.StockId);

            existing.Status = 2;
            _context.Entry(existing).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /**
         * Delete stock
         */
        public async Task DeleteStock(Stock currentStock)
        {
            var existing = await _context.Stock.FindAsync(currentStock.StockId);

            if (existing == null) throw new ArgumentException($"Count not find the Stock with ID {currentStock.StockId}");

            _context.Stock.Remove(currentStock);
            await _context.SaveChangesAsync();
        }

        /**
         * Edit the stock information
         */
        public async Task Edit(Stock stock)
        {
            _context.Entry(stock).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /**
         * Gets the total amount of stocks with the sector id 
         */
        public int GetCompanyCountBySectorId(int sectorId)
        {
            return _context.Stock.Count(x => x.Sector == sectorId);
        }

        /**
         * Gets the Total amount of Dividend by sector id
         */
        public decimal GetTotalDividendBySector(int sectorId)
        {
            return _context.Stock.Where(x => x.Sector == sectorId).Sum(x => x.DividendYield);
        }

        /**
         * Retrieve stocks by sector 
         */
        public async Task<List<Stock>> GetStockBySector(int sectorId)
        {
            return await _context.Stock.Where(x => x.Sector == sectorId).ToListAsync();
        }

        /**
         * Retrieve any stocks information that have been purchased by the user
         */
        public async Task<List<Stock>> GetStockPurchasedByUserId(string userId)
        {
            return await _context.Stock.Where(x => x.User == userId).ToListAsync();
        }

        /**
         * Returns the total amount invested in stocks 
         */
        public decimal GetTotalInvestedByUserId(string userId)
        {
            return _context.Stock.Where(x => x.User == userId).Sum(x => x.InvestmentAmount);
        }

        /**
         * Returns the total count of stocks users have in
         */
        public int GetTotalCountStockByUserId(string userId)
        {
            return _context.Stock.Count(x => x.User == userId);
        }

        /**
         * Retrieves only the stocks that the user currently owns 
         */
        public async Task<List<Stock>> GetCurrentStockByUserId(string userId)
        {
            return await _context.Stock.Where(x => x.User == userId && x.Status == 1).ToListAsync();
        }        
        
        /**
         * Retrieves the list of stocks that the user has sold 
         */
        public async Task<List<Stock>> GetStockSoldByUserId(string userId)
        {
            return await _context.Stock.Where(x => x.User == userId && x.Status == 2).ToListAsync();
        }

    }
}
