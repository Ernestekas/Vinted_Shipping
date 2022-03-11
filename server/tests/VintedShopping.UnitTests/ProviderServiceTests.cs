using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
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

            providers.Count.Should().Be(2);
            providers.SelectMany(p => p.Packages).Count().Should().Be(6);
        }

        [Fact]
        public async void GetProvidersAsync_CorrectDataWithOneInCorrectPriceFormat_ReturnValidProviders()
        {
            _inputFileServiceMock.Setup(its => its.ReadProvidersAsync())
                .ReturnsAsync(new string[]
                {
                    "LP S price",
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

                if(i == 0)
                {
                    selected.Code.Should().Be(code);
                    AssertPackage(selected.Packages[0], "M", mediumPrice);
                    AssertPackage(selected.Packages[1], "L", largePrice);
                }
                else
                {
                    AssertProvider(selected, code, smallPrice, mediumPrice, largePrice);
                }
            }

            providers.Count.Should().Be(2);
            providers.SelectMany(p => p.Packages).Count().Should().Be(5);
        }

        [Fact]
        public async void GetProvidersAsync_CorrectDataWithOneNewSize_ReturnValidProvidersWithSevenPackageTypes()
        {
            _inputFileServiceMock.Setup(its => its.ReadProvidersAsync())
                .ReturnsAsync(new string[]
                {
                    "LP S 1.50",
                    "LP M 4.90",
                    "LP L 6.90",
                    "MR S 2.00",
                    "MR M 3.00",
                    "MR L 4.00",
                    "MR XL 8.00"
                });

            List<Provider> providers = await _providerService.GetProvidersAsync();

            providers.Count.Should().Be(2);
            providers.SelectMany(p => p.Packages).Count().Should().Be(7);

            for (int i = 0; i < providers.Count; i++)
            {
                Provider selected = providers[i];
                var code = i == 0 ? "LP" : "MR";
                var smallPrice = i == 0 ? 1.50M : 2.00M;
                var mediumPrice = i == 0 ? 4.90M : 3.00M;
                var largePrice = i == 0 ? 6.90M : 4.00M;
                var xLargePrice = i == 0 ? 0 : 8.00M;

                if (i == 0)
                {
                    AssertProvider(selected, code, smallPrice, mediumPrice, largePrice);
                }
                else
                {
                    selected.Code.Should().Be(code);
                    AssertPackage(selected.Packages[0], "S", smallPrice);
                    AssertPackage(selected.Packages[1], "M", mediumPrice);
                    AssertPackage(selected.Packages[2], "L", largePrice);
                    AssertPackage(selected.Packages[3], "XL", xLargePrice);
                }
            }
        }

        [Fact]
        public async void GetProvidersAsync_GivenOnlyBadData_ReturnNoProviders()
        {
            _inputFileServiceMock.Setup(its => its.ReadProvidersAsync())
                .ReturnsAsync(new string[]
                {
                    "LP S lula",
                    "LP M sfasf",
                    "LP",
                    "MR sfasfs",
                    "MR sfas sfasf",
                    "MR L sfashgea",
                    "MR XL asf"
                });

            List<Provider> providers = await _providerService.GetProvidersAsync();
            providers.Should().BeEmpty();
        }

        private void AssertProvider(
            Provider provider, 
            string expectedCode, 
            decimal expectedSmallPrice,
            decimal expectedMediumPrice,
            decimal expectedLargePrice)
        {
            provider.Code.Should().Be(expectedCode);
            AssertPackage(provider.Packages[0], "S", expectedSmallPrice);
            AssertPackage(provider.Packages[1], "M", expectedMediumPrice);
            AssertPackage(provider.Packages[2], "L", expectedLargePrice);
        }

        private void AssertPackage(Package package, string expectedSize, decimal expectedPrice)
        {
            package.SizeAbbreviation.Should().Be(expectedSize);
            package.BasePrice.Should().Be(expectedPrice);
        }
    }
}
