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

    }
}
