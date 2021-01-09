using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diversify_Server.Models;
using Diversify_Server.Models.Database;

namespace Diversify_Server.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task AddCompany(Company newCompany);

        Company GetCompanyByCompanyId(int companyId);

        Task<Company> GetCompanyBySymbol(string symbol);

        Task<bool> CheckExistingCompany(string symbol);

        Task AddNewCompanyFromViewModel(CompanyOverviewModel newCompany, int sectorId);


    }
}
