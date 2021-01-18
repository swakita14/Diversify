using System.Collections.Generic;
using System.Threading.Tasks;
using DiversifyCL.Models.Database;

namespace DiversifyCL.Interfaces.Repositories
{
    public interface IInvestmentTotalRepository
    { 
        Task<List<InvestmentTotal>> GetAllInvestmentTotalsByUserId(string userId);

        Task<bool> CheckRemainderInvestment(string companySymbol, decimal editInvestmentAmount, string userId);

        Task AddNewTotal(InvestmentTotal newInvestmentTotal);

        Task EditInvestmentAmount(InvestmentTotal existingInvestmentTotal);

        Task<bool> CheckExistingInvestment(string companySymbol, string userId);

        Task AddNewInvestment(string companySymbol, decimal initialInvestment, int sectorId, string userId);

        Task<List<InvestmentTotal>> GetInvestmentTotalByUserId(string userId);

        Task EditExistingInvestment(string companyName, decimal editInvestmentAmount, string userId);

        Task<decimal> GetInvestedTotalByCompanySymbol(string symbol, string userId);

        Task<decimal> GetUserTotalInvestment(string userId);

    }
}