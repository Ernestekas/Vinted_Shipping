using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintedShipping.Interfaces;
using VintedShipping.Models;
using VintedShipping.Services;
using Xunit;

namespace VintedShopping.UnitTests
{
    public class ProviderServiceTests
    {
        private readonly Mock<IInputFileService> _inputFileServiceMock;
        private readonly ProviderService _providerService;

        public ProviderServiceTests()
        {
            _inputFileServiceMock = new Mock<IInputFileService>();
            _providerService = new ProviderService(_inputFileServiceMock.Object);
        }

        [Fact]
        public async void GetProvidersAsync_GivenCorrectData_ReturnCorrectOutput()
        {
            _inputFileServiceMock.Setup(its => its.ReadProvidersAsync())
                .ReturnsAsync(new string[]
                {
                    "LP S 1.50",
                    "LP M 4.90",
                    "LP L 6.90",
                    "MR S 2.00",
                    "MR M 3.00",
                    "MR L 4.00"
                });

            List<Provider> providers = await _providerService.GetProvidersAsync();
            
            for (int i = 0; i < providers.Count; i++)
            {
                Provider selected = providers[i];
                var code = i == 0 ? "LP" : "MR";
                var smallPrice = i == 0 ? 1.50M : 2.00M;
                var mediumPrice = i == 0 ? 4.90M : 3.00M;
                var largePrice = i == 0 ? 6.90M : 4.00M;

                AssertProvider(selected, code, smallPrice, mediumPrice, largePrice);
            }
            
        }

        private void AssertProvider(
            Provider provider, 
            string expectedCode, 
            decimal expectedSmallPrice,
            decimal expectedMediumPrice,
            decimal expectedLargePrice)
        {
            provider.Code.Should().Be(expectedCode);
            
            provider.Packages[0].SizeAbbreviation.Should().Be("S");
            provider.Packages[0].BasePrice.Should().Be(expectedSmallPrice);

            provider.Packages[1].SizeAbbreviation.Should().Be("M");
            provider.Packages[1].BasePrice.Should().Be(expectedMediumPrice);

            provider.Packages[2].SizeAbbreviation.Should().Be("L");
            provider.Packages[2].BasePrice.Should().Be(expectedLargePrice);
        }
    }
}
