using System.Threading.Tasks;
using Diversify_Server.Models.Database;

namespace Diversify_Server.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<bool> CheckExistingCompany(string symbol);

        Task AddNewCompany(Company newCompany);
    }
}