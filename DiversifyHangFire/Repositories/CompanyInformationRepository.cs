using System.Collections.Generic;
using System.Linq;
using DiversifyCL.Interfaces.Services;
using DiversifyCL.Models.Database;
using DiversifyCL.Models.ViewModels;
using DiversifyHangFire.Data;
using DiversifyHangFire.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DiversifyHangFire.Repositories
{
    public class CompanyInformationRepository : ICompanyInformationRepository
    {
        private readonly HangFireDbContext _context;
        private readonly ICompanyOverviewService _companyOverviewService;
        public CompanyInformationRepository(HangFireDbContext context, ICompanyOverviewService companyOverviewService)
        {
            _context = context;
            _companyOverviewService = companyOverviewService;
        }

        /**
         * Edit the existing company in DB
         */
        public void EditInformation(Company existing)
        {
            _context.Entry(existing).State = EntityState.Modified;
            _context.SaveChanges();
        }

        /**
         * Update the company information with the specific symbol
         */
        public CompanyOverviewModel GetNewOverview(string symbol)
        {
            return _companyOverviewService.GetCompanyOverviewAsync(symbol).Result;
        }

        /**
         * Get all companies in the list from the date that is the latest
         */
        public List<Company> GetAllCompanyOrderByDate()
        {
            return _context.Companies.OrderBy(x => x.DateUpdated).ToList();
        }

        /**
         * Get the company that needs the most update
         */
        public Company GetNextCompanyToUpdate()
        {
            return GetAllCompanyOrderByDate().FirstOrDefault();
        }



    }
}
