using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diversify_Server.Interfaces.Repositories;
using Diversify_Server.Interfaces.Services;
using Diversify_Server.Models.Database;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;

namespace Diversify_Server.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        /**
         * Check if there is an existing company
         */
        public async Task<bool> CheckExistingCompany(string symbol)
        {
            bool existing = false;

            if (await _companyRepository.GetCompanyBySymbol(symbol) == null)
            {
                return existing = true;
            }

            return existing;
        }

        /**
         * Add new company using repository
         */
        public async Task AddNewCompany(Company newCompany)
        { 
            await _companyRepository.AddCompany(newCompany);
        }

    }

}
