using System.Collections.Generic;
using System.Threading.Tasks;
using Diversify_Server.Models.Database;

namespace Diversify_Server.Interfaces.Repositories
{
    public interface IInvestmentTotalRepository
    { 
        Task<List<InvestmentTotal>> GetAllInvestmentTotalsByUserId(string userId);

        Task AddNewTotal(InvestmentTotal newInvestmentTotal);

        Task EditInvestmentAmount(InvestmentTotal existingInvestmentTotal);
    }
}