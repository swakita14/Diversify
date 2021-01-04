using Diversify_Server.Interfaces.Repositories;
using Diversify_Server.Interfaces.Services;
using Diversify_Server.Models.Database;
using Diversify_Server.Repositories;
using Diversify_Server.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task GetInvestedTotalByCompanySymbol_GivenSymbol_ReturnsTotal()
        {
            // Arragne 
            var userStocks = new List<InvestmentTotal>
            {
                new InvestmentTotal { InvestmentTotalId = 1, Symbol = "AAPL", InvestedAmount = 100},
            };

            _investmentTotalRepositoryMock.Setup(x => x.GetAllInvestmentTotalsByUserId(It.IsAny<string>())).Returns(Task.FromResult(userStocks));

            // Act
            var results = await _sut.GetInvestedTotalByCompanySymbol("AAPL");

            // Assert 
            results.Should().Be(100);
        }

        [Test]
        public async Task GetInvestmentTotalByUserId_GivenId_ReturnsInvestmentTotalList()
        {
            // Arrange 
            var investmentTotal = new List<InvestmentTotal>
            {
                new InvestmentTotal { InvestmentTotalId = 1, Symbol = "AAPL", InvestedAmount = 100},
                new InvestmentTotal { InvestmentTotalId = 2, Symbol = "IBM", InvestedAmount = 200},
            };

            _investmentTotalRepositoryMock.Setup(x => x.GetAllInvestmentTotalsByUserId(It.IsAny<string>())).Returns(Task.FromResult(investmentTotal));

            // Act
            var result = await _sut.GetInvestmentTotalByUserId();

            // Assert
            result.Count.Should().Be(2);
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

            _investmentTotalRepositoryMock.Setup(x => x.GetAllInvestmentTotalsByUserId(It.IsAny<string>())).Returns(Task.FromResult(investmentTotal));

            // Act
            var result = await _sut.GetUserTotalInvestment();

            // Assert
            result.Should().Be(300);
        }

        [Test]
        public async Task CheckExistingInvestment_GivenSymbolOfExisting_ReturnsTrue()
        {
            // Arragne 
            var userStocks = new List<InvestmentTotal>
            {
                new InvestmentTotal { InvestmentTotalId = 1, Symbol = "AAPL", InvestedAmount = 100},
            };

            _investmentTotalRepositoryMock.Setup(x => x.GetAllInvestmentTotalsByUserId(It.IsAny<string>())).Returns(Task.FromResult(userStocks));

            // Act 
            var results = await _sut.CheckExistingInvestment("AAPL");

            // Assert
            Assert.AreEqual(results, true);
        }

        [Test]
        public async Task CheckExistingInvestment_GivenSymbolOfNonExisting_ReturnsFalse()
        {
            // Arragne 
            var userStocks = new List<InvestmentTotal>
            {
                new InvestmentTotal { InvestmentTotalId = 1, Symbol = "AAPL", InvestedAmount = 100},
            };

            _investmentTotalRepositoryMock.Setup(x => x.GetAllInvestmentTotalsByUserId(It.IsAny<string>())).Returns(Task.FromResult(userStocks));

            // Act 
            var results = await _sut.CheckExistingInvestment("IBM");

            // Assert
            Assert.AreEqual(results, false);
        }

        [Test]
        public async Task CheckRemainderInvestment_GivenLargerThanCurrentInvestment_ReturnsFalse()
        {
            // Arragne 
            var userStocks = new List<InvestmentTotal>
            {
                new InvestmentTotal { InvestmentTotalId = 1, Symbol = "AAPL", InvestedAmount = 100},
            };

            _investmentTotalRepositoryMock.Setup(x => x.GetAllInvestmentTotalsByUserId(It.IsAny<string>())).Returns(Task.FromResult(userStocks));

            // Act
            var results = await _sut.CheckRemainderInvestment("AAPL", 150);

            // Assert 
            Assert.AreEqual(results, false);
        }        
        
        [Test]
        public async Task CheckRemainderInvestment_GivenSmallerThanCurrentInvestment_ReturnsTrue()
        {
            // Arragne 
            var userStocks = new List<InvestmentTotal>
            {
                new InvestmentTotal { InvestmentTotalId = 1, Symbol = "AAPL", InvestedAmount = 100},
            };

            _investmentTotalRepositoryMock.Setup(x => x.GetAllInvestmentTotalsByUserId(It.IsAny<string>())).Returns(Task.FromResult(userStocks));

            // Act
            var results = await _sut.CheckRemainderInvestment("AAPL", 50);

            // Assert 
            Assert.AreEqual(results, true);
        }

    }
}
