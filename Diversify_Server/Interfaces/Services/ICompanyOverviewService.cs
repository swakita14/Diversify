using System.Threading.Tasks;
using Diversify_Server.Models;

namespace Diversify_Server.Interfaces.Services
{
    public interface ICompanyOverviewService
    {
        Task<CompanyOverviewModel> GetCompanyOverviewAsync(string symbol);
    }
}