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
         * Add new company using repository
         */
        public async Task AddNewCompany(Company newCompany)
        { 
            await _companyRepository.AddCompany(newCompany);
        }

        public Company GetCompanyByCompanyId(int companyId)
        {
            return  _companyRepository.GetCompanyByCompanyId(companyId);
        }
        
        public async Task<Company> GetCompanyByCompanyIdAsync(int companyId)
        {
            return  await _companyRepository.GetCompanyByCompanyIdAsync(companyId);
        }

    }

}
