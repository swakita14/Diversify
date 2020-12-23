using System.Collections.Generic;
using System.Threading.Tasks;
using Diversify_Server.Models.Database;

namespace Diversify_Server.Interfaces.Services
{
    public interface IInvestmentTotalService
    {
        Task<decimal> GetInvestedTotalByCompanySymbol(string symbol);

        Task<bool> CheckExistingInvestment(string companySymbol);

        Task AddNewInvestment(string companySymbol, decimal initialInvestment, int sectorId);

        Task<List<InvestmentTotal>> GetInvestmentTotalByUserId();

        Task EditExistingInvestment(string companySymbol, decimal editInvestmentAmount);

        Task<bool> CheckRemainderInvestment(string companySymbol, decimal editInvestmentAmount);

        Task<decimal> GetUserTotalInvestment();
    }
}