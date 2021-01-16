
using System.Threading.Tasks;
using DiversifyCL.Interfaces.Repositories;
using DiversifyCL.Interfaces.Services;
using DiversifyCL.Models.Database;

namespace DiversifyCL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        /**
         * Add new company using repository
         */
        public async Task AddNewCompany(Company newCompany)
        { 
            await _companyRepository.AddCompany(newCompany);
        }

        public Company GetCompanyByCompanyId(int companyId)
        {
            return  _companyRepository.GetCompanyByCompanyId(companyId);
        }
        
        public async Task<Company> GetCompanyByCompanyIdAsync(int companyId)
        {
            return  await _companyRepository.GetCompanyByCompanyIdAsync(companyId);
        }

    }

}
