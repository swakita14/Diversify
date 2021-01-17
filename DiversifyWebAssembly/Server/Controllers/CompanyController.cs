using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiversifyCL.Interfaces.Services;

namespace DiversifyWebAssembly.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyOverviewService _companyOverviewService;

        public CompanyController(ICompanyService companyService, ICompanyOverviewService companyOverviewService)
        {
            _companyService = companyService;
            _companyOverviewService = companyOverviewService;
        }

        [Route("/symbol/{companySymbol}")]
        [HttpGet]
        public async Task<IActionResult> GetCompanyOverview(string companySymbol)
        {
            var overview = await _companyOverviewService.GetCompanyOverviewAsync(companySymbol);

            return Ok(overview);
        }
    }
}
