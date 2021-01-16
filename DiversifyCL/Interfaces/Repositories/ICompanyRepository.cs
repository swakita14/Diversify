
using System.Threading.Tasks;
using DiversifyCL.Models.Database;
using DiversifyCL.Models.ViewModels;

namespace DiversifyCL.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task AddCompany(Company newCompany);

        Company GetCompanyByCompanyId(int companyId);
        Task<Company> GetCompanyByCompanyIdAsync(int companyId);

        Task<Company> GetCompanyBySymbol(string symbol);

        Task<bool> CheckExistingCompany(string symbol);

        Task AddNewCompanyFromViewModel(CompanyOverviewModel newCompany, int sectorId);

        int GetCompanyCountBySectorId(int sectorId);

        decimal GetTotalDividendBySector(int sectorId);


    }
}
