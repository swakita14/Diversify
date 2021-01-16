using System.Threading.Tasks;
using DiversifyCL.Models.Database;

namespace DiversifyCL.Interfaces.Services
{
    public interface ICompanyService
    {
        Task AddNewCompany(Company newCompany);

        Company GetCompanyByCompanyId(int companyId);
        Task<Company> GetCompanyByCompanyIdAsync(int companyId);
    }
}