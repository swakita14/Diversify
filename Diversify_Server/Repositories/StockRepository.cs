using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Diversify_Server.Data;
using Diversify_Server.Interfaces.Repositories;
using DiversifyCL.Models.Database;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.Schedule.Internal;

namespace Diversify_Server.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly DiversifyContext _context;
        private readonly ICompanyRepository _companyRepository;

        public StockRepository(DiversifyContext context, ICompanyRepository companyRepository)
        {
            _context = context;
            _companyRepository = companyRepository;
        }

        /**
         *  Adding Stock
         */
        public async Task AddStock(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        /**
         * Method to sell stocks 
         */
        public async Task SellStock(Stock currentStock)
        {
            var existing = await _context.Stocks.FindAsync(currentStock.StockId);

            existing.Status = 2;
            _context.Entry(existing).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /**
         * Delete stock
         */
        public async Task DeleteStock(Stock currentStock)
        {
            var existing = await _context.Stocks.FindAsync(currentStock.StockId);

            if (existing == null) throw new ArgumentException($"Count not find the Stock with ID {currentStock.StockId}");

            _context.Stocks.Remove(currentStock);
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
         * Retrieves only the stocks that the user currently owns 
         */
        public async Task<List<Stock>> GetCurrentStockByUserId(string userId)
        {
            return await _context.Stocks.Where(x => x.User == userId && x.Status == 1).ToListAsync();
        }



        /**
         * Retrieves the list of stocks that the user has sold 
         */
        public async Task<List<Stock>> GetStockSoldByUserId(string userId)
        {
            return await _context.Stocks.Where(x => x.User == userId && x.Status == 2).ToListAsync();
        }

        /**
         * Gets stock with the company symbol
         */
        public async Task<Stock> GetStockByCompanySymbol(string symbol)
        {
            var companyInformation = await _context.Companies.FirstOrDefaultAsync(x => x.Symbol == symbol);

            return await _context.Stocks.FirstOrDefaultAsync(x => x.Company == companyInformation.CompanyId);
        }


    }
}
