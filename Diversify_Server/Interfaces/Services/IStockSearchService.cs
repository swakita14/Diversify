using System.Threading.Tasks;
using Diversify_Server.Models;

namespace Diversify_Server.Interfaces
{
    public interface IStockSearchService
    {
        Task<SearchModelList> GetStockAsync(string keyword);
    }
}