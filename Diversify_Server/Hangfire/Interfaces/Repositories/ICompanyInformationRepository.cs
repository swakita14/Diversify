using System.Collections.Generic;
using Diversify_Server.Models;
using Diversify_Server.Models.Database;

namespace Diversify_Server.HangFire.Interfaces.Repositories
{
    public interface ICompanyInformationRepository
    { 
        CompanyOverviewModel GetNewOverview(string symbol);
        void EditInformation(Company existing);

        Company GetNextCompanyToUpdate();
        List<Company> GetAllCompanyOrderByDate();

    }
}