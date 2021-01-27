using System.Collections.Generic;
using System.Threading.Tasks;
using DiversifyCL.Models.Database;

namespace DiversifyCL.Interfaces.Repositories
{
    public interface IInvestmentTrendRepository
    {
        Task<bool> CheckRemainderInvestment(int companyId, decimal editInvestmentAmount, string userId);

        Task AddNewTotal(InvestmentTrend newInvestmentTotal);

        Task EditInvestmentAmount(InvestmentTrend existingInvestmentTotal);

        Task<bool> CheckExistingInvestment(int companyId, string userId);

        Task AddNewInvestment(int companyId, decimal initialInvestment, int sectorId, string userId);

        Task<List<InvestmentTrend>> GetAllInvestmentByUserId(string userId);

        Task<decimal> GetInvestedTotalByCompany(int companyId, string userId);

        Task<decimal> GetUserTotalInvestment(string userId);

    }
}