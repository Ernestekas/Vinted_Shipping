using Moq;
using System;
using VintedShipping.Interfaces;
using VintedShipping.Services;
using Xunit;

namespace VintedShopping.UnitTests
{
    public class TransactionServiceTests
    {
        private const decimal _maxTotalDiscountsAllowed = 10.00M;

        private int _largePackCountCurrentMonth = 0;
        private decimal _totalMonthlyDiscount = 0.00M;
        private int _discountMonth = 0;
        private int _maxLargePacksCountToDiscount = 1;
        private int _discountLargePackPerLargePacksCount = 3;
        private int _largePacksDiscounted = 0;

        private readonly TransactionService _transactionService;
        private readonly Mock<IInputFileService> _inputFileServiceMock;
        private readonly Mock<IProviderService> _providerServiceMock;

        public TransactionServiceTests()
        {
            _inputFileServiceMock = new Mock<IInputFileService>();
            _providerServiceMock = new Mock<IProviderService>();

            _transactionService = new TransactionService(_inputFileServiceMock.Object, _providerServiceMock.Object);
        }

        [Fact]
        public void GetTransactionsWithDiscounts()
        {
            
        }
    }
}
