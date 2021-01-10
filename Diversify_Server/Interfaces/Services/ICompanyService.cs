using System.Threading.Tasks;
using Diversify_Server.Models.Database;

namespace Diversify_Server.Interfaces.Services
{
    public interface ICompanyService
    {
        Task AddNewCompany(Company newCompany);

        Company GetCompanyByCompanyId(int companyId);
        Task<Company> GetCompanyByCompanyIdAsync(int companyId);
    }
}