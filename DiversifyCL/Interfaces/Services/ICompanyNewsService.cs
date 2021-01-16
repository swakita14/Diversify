using System.Threading.Tasks;
using DiversifyCL.Models;

namespace DiversifyCL.Interfaces.Services
{
    public interface ICompanyNewsService
    {
        Task<NewsSearchResult> GetCompanyNewsAsync(string keyword);
    }
}