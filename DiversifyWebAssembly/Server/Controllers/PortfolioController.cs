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
    public class PortfolioController : ControllerBase
    {
        private readonly IStockPortfolioService _stockPortfolioService;
        public PortfolioController(IStockPortfolioService stockPortfolioService)
        {
            _stockPortfolioService = stockPortfolioService;
        }

        [Route("/transaction/sold")]
        [HttpGet]
        public async Task<IActionResult> GetUserStockSoldTransaction()
        {
            var soldStockList = await _stockPortfolioService.GetCurrentUserStockTransactionSold();

            return Ok(soldStockList);
        }

        [Route("/transaction/all")]
        public async Task<IActionResult> GetUserTransaction()
        {
            var transaction = await _stockPortfolioService.GetCurrentUserStockTransaction();

            return Ok(transaction);
        }

        [Route("/by/company")]
        [HttpGet]
        public async Task<IActionResult> GetPortfolioByCompany()
        {
            var groupByCompany = await _stockPortfolioService.StockPortfolioGroupByCompany();

            return Ok(groupByCompany);
        }        
        
        [Route("/by/sector")]
        [HttpGet]
        public async Task<IActionResult> GetPortfolioBySector()
        {
            var groupBySector = await _stockPortfolioService.StockPortfolioGroupBySector();

            return Ok(groupBySector);
        }
    }
}
