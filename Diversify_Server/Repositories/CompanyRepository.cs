﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diversify_Server.Data;
using Diversify_Server.Interfaces.Repositories;
using Diversify_Server.Models;
using Diversify_Server.Models.Database;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.EntityFrameworkCore;

namespace Diversify_Server.Repositories
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

            if (existing == null) throw new ArgumentException($"Count not find the company with symbol: {symbol}");

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
                PERatio = decimal.Parse(company.PERatio)
            };

            await AddCompany(newCompany);
        }

    }
}
