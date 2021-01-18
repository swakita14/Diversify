using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiversifyCL.Interfaces.Services
{
    public interface IInvestmentTotalService
    {
        Task<decimal> GetInvestmentTotalWithCompanySymbol(string symbol);

    }
}