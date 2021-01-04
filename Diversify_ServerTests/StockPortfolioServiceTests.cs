using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Diversify_Server.Interfaces.Repositories;
using Diversify_Server.Interfaces.Services;
using Diversify_Server.Models.Database;
using Diversify_Server.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Diversify_ServerTests
{
    [TestFixture]
    public class StockPortfolioServiceTests
    {
        private readonly Mock<IStockRepository> _stockRepository;
        private readonly Mock<ISectorRepository> _sectorRepository;
        private readonly Mock<IIdentityService> _identityService;
        private readonly Mock<IInvestmentTotalService> _investmentTotalService;

        private readonly StockPortfolioService _sut;

        public StockPortfolioServiceTests()
        {
            _stockRepository = new Mock<IStockRepository>();
            _sectorRepository = new Mock<ISectorRepository>();
            _identityService = new Mock<IIdentityService>();
            _investmentTotalService = new Mock<IInvestmentTotalService>();

            _sut = new StockPortfolioService(_stockRepository.Object, _sectorRepository.Object, _identityService.Object, _investmentTotalService.Object);
        }

        [Test]
        public async Task GetCurrentUserStockTransaction_ShouldReturnAllTransactions()
        {
            // Arrange 
            var userStocks = new List<Stock>
            {
                new Stock
                {
                    StockId = 1, InvestmentAmount = 100, Symbol = "AAPL", Sector = 1, PurchaseDate = DateTime.Now,
                    DividendYield = 0, ExDividendDate = DateTime.Now
                },               
                new Stock
                {
                    StockId = 2, InvestmentAmount = 100, Symbol = "IBM", Sector = 1, PurchaseDate = DateTime.Now,
                    DividendYield = 0, ExDividendDate = DateTime.Now
                }
                
            };

            _stockRepository.Setup(x => x.GetCurrentStockByUserId(It.IsAny<string>()))
                .Returns(Task.FromResult(userStocks));

            // Act 
            var results = await _sut.GetCurrentUserStockTransaction();

            // Assert 
            results.Count.Should().Be(2);
        }

        [Test]
        public async Task GetCompanyInformationByStockSymbol_GivenSymbol_ShouldReturnCompanyInformation()
        {
            // Arrange 
            var userStocks = new Stock
            {
                StockId = 1,
                InvestmentAmount = 100,
                Symbol = "AAPL",
                Sector = 1,
                PurchaseDate = DateTime.Now,
                DividendYield = 0,
                ExDividendDate = DateTime.Now
            };

            _stockRepository.Setup(x => x.GetCompanyBySymbol(It.IsAny<string>())).Returns(Task.FromResult(userStocks));

            // Act 
            var results = await _sut.GetCompanyInformationByStockSymbol("AAPL");

            // Assert 
            Assert.AreEqual(results.InvestmentAmount, 100);
            Assert.AreEqual(results.StockId, 1);
        }
    }
}
