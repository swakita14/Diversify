
using DiversifyCL.Interfaces.Repositories;
using DiversifyCL.Interfaces.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiversifyCL.Models.Database;
using DiversifyCL.Services;

namespace Diversify_ServerTests
{
    [TestFixture]
    public class InvestmentTotalServiceTests
    {
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<IInvestmentTotalRepository> _investmentTotalRepositoryMock;
        private readonly InvestmentTotalService _sut;

        public InvestmentTotalServiceTests()
        {
            _identityServiceMock = new Mock<IIdentityService>();
            _investmentTotalRepositoryMock = new Mock<IInvestmentTotalRepository>();

            _sut = new InvestmentTotalService(_investmentTotalRepositoryMock.Object, _identityServiceMock.Object);
        }

        [Test]
        public async Task GetUserTotalInvestment_GivenId_ReturnsTotalInvestment()
        {
            // Arrange 
            var investmentTotal = new List<InvestmentTotal>
            {
                new InvestmentTotal { InvestmentTotalId = 1, Symbol = "AAPL", InvestedAmount = 100},
                new InvestmentTotal { InvestmentTotalId = 2, Symbol = "IBM", InvestedAmount = 200},
            };

            _investmentTotalRepositoryMock.Setup(x => x.GetInvestmentTotalByUserId(It.IsAny<string>())).Returns(Task.FromResult(investmentTotal));

            // Act
            var result = await _sut.GetUserTotalInvestment();

            // Assert
            result.Should().Be(300);
        }

        [Test]
        public async Task GetUserTotalInvestment_ShouldReturnTotalInvestmentAmount()
        {
            // Arragne 
            var userStocks = new List<InvestmentTotal>
            {
                new InvestmentTotal { InvestmentTotalId = 1, Symbol = "AAPL", InvestedAmount = 100},
                new InvestmentTotal { InvestmentTotalId = 2, Symbol = "IBM", InvestedAmount = 200},
            };

            _investmentTotalRepositoryMock.Setup(x => x.GetInvestmentTotalByUserId(It.IsAny<string>()))
                .Returns(Task.FromResult(userStocks));

            // Act 
            var results = await _sut.GetUserTotalInvestment();

            // Assert 
            Assert.AreEqual(results, 300);
        }

        [Test]
        public async Task GetInvestmentTotalWithCompanySymbol_GivenCompanySymbol_ShouldReturnCompanyInvestedAmount()
        {
            //Arrange 
            var userStocks = new List<InvestmentTotal>
            {
                new InvestmentTotal { InvestmentTotalId = 1, Symbol = "AAPL", InvestedAmount = 100},
            };

            _investmentTotalRepositoryMock.Setup(x => x.GetInvestedTotalByCompanySymbol("AAPL", It.IsAny<string>()))
                .Returns(Task.FromResult(userStocks.Sum(x => x.InvestedAmount)));

            // Act 
            var results = await _sut.GetInvestmentTotalWithCompanySymbol("AAPL");

            // Assert 
            Assert.AreEqual(results, 100);
        }

    }
}
