using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiversifyCL.Interfaces.Services;
using DiversifyCL.Models.ViewModels;
using DiversifyWebAssembly.Client.Model;

namespace DiversifyWebAssembly.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        public StockController(IStockService  stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        public async Task<IActionResult> AddStock([FromBody] AddStockViewModel newStock)
        {
            CompanyOverviewModel model = new CompanyOverviewModel
            {
                Name = newStock.CompanyName,
                Symbol = newStock.CompanySymbol,
                DividendYield = newStock.DividendYield,
                ExDividendDate = newStock.ExDividendDate,
                EPS = newStock.EPS,
                Exchange = newStock.Exchange,
                PayoutRatio = newStock.PayoutRatio,
                PERatio = newStock.PERatio,
                Sector = newStock.Sector
            };

            await _stockService.AddStockAsync(model, newStock.InvestmentAmount, newStock.PurchaseDateTime);

            return Ok();
        }
    }
}
