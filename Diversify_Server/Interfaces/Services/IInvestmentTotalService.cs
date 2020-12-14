using System.Threading.Tasks;

namespace Diversify_Server.Interfaces.Services
{
    public interface IInvestmentTotalService
    {
        Task<decimal> GetInvestedTotalByCompanySymbol(string symbol);

        Task<bool> CheckExistingInvestment(string companySymbol);

        Task AddNewInvestment(string companySymbol, decimal initialInvestment, int sectorId);

        Task EditExistingInvestment(string companySymbol, decimal editInvestmentAmount);

        Task<bool> CheckRemainderInvestment(string companySymbol, decimal editInvestmentAmount);
    }
}