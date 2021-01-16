using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diversify_Server.Interfaces.Services
{
    public interface IInvestmentTotalService
    {
        Task<bool> CheckRemainderInvestment(string companySymbol, decimal editInvestmentAmount);

        Task<decimal> GetInvestmentTotalWithCompanySymbol(string symbol);

    }
}