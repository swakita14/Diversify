using System.Threading.Tasks;
using Diversify_Server.Models;

namespace Diversify_Server.Interfaces.Services
{
    public interface IStockSearchService
    {
        Task<SearchModelList> GetStockAsync(string keyword);
    }
}