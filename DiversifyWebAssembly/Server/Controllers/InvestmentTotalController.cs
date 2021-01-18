using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DiversifyCL.Interfaces.Services;

namespace DiversifyWebAssembly.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentTotalController : ControllerBase
    {
        private readonly IInvestmentTotalService _investmentTotalService;

        public InvestmentTotalController(IInvestmentTotalService investmentTotalService)
        {
            _investmentTotalService = investmentTotalService;
        }

        [Route("/total/{companyName}")]
        [HttpGet]
        public async Task<IActionResult> GetInvestmentTotalBySymbol(string companyName)
        {
            var investmentTotals = await _investmentTotalService.GetInvestmentTotalWithCompanySymbol(companyName);

            return Ok(investmentTotals);
        }
    }
}
