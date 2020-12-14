using System.Threading.Tasks;

namespace Diversify_Server.Interfaces.Services
{
    public interface IInvestmentTotalService
    {
        Task<decimal> GetInvestedTotalByCompanySymbol(string symbol);

        Task<bool> CheckExistingInvestment(string companySymbol);

        Task AddNewInvestment(string companyName, decimal initialInvestment, int sectorId);

        Task EditExistingInvestment(string companyName, decimal editInvestmentAmount);
    }
}