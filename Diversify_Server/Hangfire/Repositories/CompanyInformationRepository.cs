using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diversify_Server.Data;
using Diversify_Server.HangFire.Interfaces;
using Diversify_Server.HangFire.Interfaces.Repositories;
using Diversify_Server.Interfaces.Services;
using Diversify_Server.Models;
using Diversify_Server.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Diversify_Server.HangFire.Repositories
{
    public class CompanyInformationRepository : ICompanyInformationRepository
    {
        private readonly DiversifyContext _context;
        private readonly ICompanyOverviewService _companyOverviewService;
        public CompanyInformationRepository(DiversifyContext context, ICompanyOverviewService companyOverviewService)
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
