using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiversifyCL.Interfaces.Repositories;
using DiversifyCL.Interfaces.Services;
using DiversifyCL.Models.Database;
using DiversifyCL.Services;
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
        private readonly Mock<ICompanyRepository> _companyRepository;
        private readonly Mock<IInvestmentTotalRepository> _investmentTotalRepository;

        private readonly StockPortfolioService _sut;

        public StockPortfolioServiceTests()
        {
            _stockRepository = new Mock<IStockRepository>();
            _sectorRepository = new Mock<ISectorRepository>();
            _identityService = new Mock<IIdentityService>();
            _companyRepository = new Mock<ICompanyRepository>();
            _investmentTotalRepository = new Mock<IInvestmentTotalRepository>();

            _sut = new StockPortfolioService(_stockRepository.Object, _sectorRepository.Object, _identityService.Object, _investmentTotalRepository.Object, _companyRepository.Object);
        }

        [Test]
        public async Task GetCurrentUserStockTransaction_ShouldReturnAllTransactions()
        {
            // Arrange 
            var userStocks = new List<Stock>
            {
                new Stock
                {
                    StockId = 1, InvestmentAmount = 100, Company = 1, PurchaseDate = DateTime.Now, Status = 2
                },
                new Stock
                {
                    StockId = 2, InvestmentAmount = 100, Company = 1, PurchaseDate = DateTime.Now, Status = 2
                }
            };

            var company = new Company
            {
                CompanyId = 1,
                Symbol = "AAPL",
                DividendYield = .5M,
                Sector = 1
            };

            _companyRepository.Setup(x => x.GetCompanyByCompanyId(It.IsAny<int>()))
                .Returns(company);

            _stockRepository.Setup(x => x.GetCurrentStockByUserId(It.IsAny<string>()))
                .Returns(Task.FromResult(userStocks));

            // Act 
            var results = await _sut.GetCurrentUserStockTransaction();

            // Assert 
            results.Count.Should().Be(2);
        }


        [Test]
        public async Task GetCurrentUserStockTransactionSold_GivenUserId_ShouldReturnSoldStockList()
        {
            // Arrange 
            var userStockSoldList = new List<Stock>
            {
                new Stock
                {
                    StockId = 1, InvestmentAmount = 100, Company = 1, PurchaseDate = DateTime.Now, Status = 2, User = "John"
                },
                new Stock
                {
                    StockId = 1, InvestmentAmount = 100, Company = 1, PurchaseDate = DateTime.Now, Status = 2, User = "John"
                }
            };

            var company = new Company
            {
                CompanyId = 1, Symbol = "AAPL", DividendYield = .5M, Sector = 1
            };

            _stockRepository.Setup(x => x.GetStockSoldByUserId(It.IsAny<string>()))
                .Returns(Task.FromResult(userStockSoldList));

            _companyRepository.Setup(x => x.GetCompanyByCompanyId(It.IsAny<int>()))
                .Returns(company);

            // Act 
            var results = await _sut.GetCurrentUserStockTransactionSold();

            // Assert 
            results.Count.Should().Be(2);
        }

    }
}
