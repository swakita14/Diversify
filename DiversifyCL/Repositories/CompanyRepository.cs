using System;
using System.Linq;
using System.Threading.Tasks;
using DiversifyCL.Data;
using DiversifyCL.Interfaces.Repositories;
using DiversifyCL.Models;
using DiversifyCL.Models.Database;
using DiversifyCL.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DiversifyCL.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DiversifyContext _context;
        public CompanyRepository(DiversifyContext context)
        {
            _context = context;
        }
        
        /**
         * Add new company 
         */
        public async Task AddCompany(Company newCompany)
        {
            await _context.Companies.AddAsync(newCompany);
            await _context.SaveChangesAsync();
        }

        /**
         * Get Company information by company Id
         */
        public Company GetCompanyByCompanyId(int companyId)
        {
            var existing =  _context.Companies.Find(companyId);

            if (existing == null) throw new ArgumentException($"Count not find the company with Id: {companyId}");

            return existing;
        }


        /**
         * Get the company and the company information with the symbol
         */
        public async Task<Company> GetCompanyBySymbol(string symbol)
        { 
            var existing = await _context.Companies.FirstOrDefaultAsync(x => x.Symbol == symbol);

            return existing;
        }

        /**
         * Check if the specific company information is stored in the database
         * Returns true if exist, returns false if otherwise
         */
        public async Task<bool> CheckExistingCompany(string symbol)
        {
            var existing = await GetCompanyBySymbol(symbol);

            if (existing is null) return false;

            return true;
        }

        public async Task AddNewCompanyFromViewModel(CompanyOverviewModel company, int sectorId)
        {
            Company newCompany = new Company
            {
                Name = company.Name,
                Symbol = company.Symbol,
                DividendYield = decimal.Parse(company.DividendYield),
                ExDividendDate = DateTime.Parse(company.ExDividendDate),
                EPS = decimal.Parse(company.EPS),
                Exchange = company.Exchange,
                PayoutRatio = decimal.Parse(company.PayoutRatio),
                Sector = sectorId,
                PERatio = decimal.Parse(company.PERatio),
                DateUpdated = DateTime.Now
            };

            await AddCompany(newCompany);
        }


        public int GetCompanyCountBySectorId(int sectorId)
        {
            return _context.Companies.Count(x => x.Sector == sectorId);
        }

        public  decimal GetTotalDividendBySector(int sectorId)
        {
            var existing =  _context.Companies.Where(x => x.Sector == sectorId).Sum(x => x.DividendYield);

            return existing;
        }

        public async Task<Company> GetCompanyByCompanyIdAsync(int companyId)
        {
            return await _context.Companies.FindAsync(companyId);
        }



    }
}
