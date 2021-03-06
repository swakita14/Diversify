﻿using System.Collections.Generic;
using DiversifyCL.Models.Database;
using DiversifyCL.Models.ViewModels;

namespace DiversifyHangFire.Interfaces.Repositories
{
    public interface ICompanyInformationRepository
    { 
        CompanyOverviewModel GetNewOverview(string symbol);
        void EditInformation(Company existing);

        Company GetNextCompanyToUpdate();
        List<Company> GetAllCompanyOrderByDate();

    }
}