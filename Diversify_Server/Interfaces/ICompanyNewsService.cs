using System.Threading.Tasks;
using Diversify_Server.Models;

namespace Diversify_Server.Interfaces
{
    public interface ICompanyNewsService
    {
        Task<NewsSearchResult> GetCompanyNewsAsync(string keyword);
    }
}