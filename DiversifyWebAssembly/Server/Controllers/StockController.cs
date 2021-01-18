using System;
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
        private readonly IIdentityService _identityService;
        public StockController(IStockService  stockService, IIdentityService identityService)
        {
            _stockService = stockService;
            _identityService = identityService;
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

        [HttpPost]
        public async Task<IActionResult> SellStock([FromBody] SellStockViewModel existing)
        {
            await _stockService.SellStock(existing.CompanySymbol, existing.Amount, DateTime.Now);

            return Ok();
        }

        [Route("/search/{companySymbol}")]
        [HttpGet]
        public async Task<IActionResult> GetStock(string companyName)
        {
            var searchResult = await _stockService.GetStockAsync(companyName);

            return Ok(searchResult);
        }

    }
}
