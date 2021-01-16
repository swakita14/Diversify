using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiversifyCL.Interfaces.Services
{
    public interface IInvestmentTotalService
    {
        Task<bool> CheckRemainderInvestment(string companySymbol, decimal editInvestmentAmount);

        Task<decimal> GetInvestmentTotalWithCompanySymbol(string symbol);

    }
}