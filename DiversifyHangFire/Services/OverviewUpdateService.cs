
using System;
using DiversifyCL.Interfaces.Services;
using DiversifyCL.Models.ViewModels;
using DiversifyHangFire.Interfaces.Repositories;
using DiversifyHangFire.Interfaces.Services;

namespace DiversifyHangFire.Services
{
    public class OverviewUpdateService : IOverviewUpdateService
    {

        private readonly ICompanyInformationRepository _companyInformationRepository;
        public OverviewUpdateService(ICompanyInformationRepository companyInformationRepository, ICompanyOverviewService companyOverviewService)
        {
            _companyInformationRepository = companyInformationRepository;
        }

        /**
         * Get the company that needs the most update, and then edit the company with new information 
         */
        public void UpdateCompanyOverview()
        {
            // Get the company to update next
            var companyUpdate = _companyInformationRepository.GetNextCompanyToUpdate();

            // Send the company symbol to external API endpoint to get new data
            CompanyOverviewModel newOverview = _companyInformationRepository.GetNewOverview(companyUpdate.Symbol);

            // Update the company information 
            companyUpdate.DateUpdated = DateTime.Now;
            companyUpdate.DividendYield = decimal.Parse(newOverview.DividendYield);
            companyUpdate.EPS = decimal.Parse(newOverview.EPS);
            companyUpdate.PERatio = decimal.Parse(newOverview.PERatio);
            companyUpdate.ExDividendDate = DateTime.Parse(newOverview.ExDividendDate);
            companyUpdate.ProfitMargin = decimal.Parse(newOverview.ProfitMargin);
            companyUpdate.PayoutRatio = decimal.Parse(newOverview.PayoutRatio);

            // Edit the information 
            _companyInformationRepository.EditInformation(companyUpdate);
        }
    }
}
