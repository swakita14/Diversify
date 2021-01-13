using System.Threading.Tasks;
using DiversifyCL.Models;

namespace DiversifyCL.Interfaces.Services
{
    public interface ICompanyOverviewService
    {
        Task<CompanyOverviewModel> GetCompanyOverviewAsync(string symbol);
    }
}