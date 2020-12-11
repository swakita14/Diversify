using System.Threading.Tasks;

namespace Diversify_Server.Interfaces.Services
{
    public interface IInvestmentTotalService
    {
        Task<decimal> GetInvestedTotalByCompanySymbol(string symbol);
    }
}