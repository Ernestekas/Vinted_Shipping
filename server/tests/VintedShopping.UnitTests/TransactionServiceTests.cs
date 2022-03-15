using FluentAssertions;
using Moq;
using System.Collections.Generic;
using VintedShipping.Interfaces;
using VintedShipping.Models;
using VintedShipping.Services;
using VintedShopping.UnitTests.ExpectedResults;
using Xunit;

namespace VintedShopping.UnitTests
{
    public class TransactionServiceTests
    {
        private readonly TransactionService _transactionService;
        private readonly Mock<IInputFileService> _inputFileServiceMock;
        private readonly ProviderService _providerService;

        public TransactionServiceTests()
        {
            _inputFileServiceMock = new Mock<IInputFileService>();
            _providerService = new ProviderService(_inputFileServiceMock.Object);
            _transactionService = new TransactionService(_inputFileServiceMock.Object, _providerService);
        }

        [Fact]
        public async void GetTransactionsWithDiscounts_GivenDefaultInput_ReturnCorrectTransactionsData()
        {
            SetupInputFileServiceMock();

            List<Transaction> transactionList = await _transactionService.GetTransactionsWithDiscounts();
            List<Transaction> expectedTransactions = ExpectedTransactions.GetExpectedTransactions();

            transactionList.Count.Should().Be(21);
            AssertTransactions(transactionList, expectedTransactions);
        }

        [Fact]
        public async void GetTransactionsWithDiscounts_GivenOnlyInvalidData_ReturnCorrectTransactionsData()
        {
            SetupInputFileServiceMockForOnlyInvalidData();

            List<Transaction> transactions = await _transactionService.GetTransactionsWithDiscounts();
            List<Transaction> expectedTransactions = ExpectedTransactions.GetDefaultAllInvalidTransactions();

            transactions.Count.Should().Be(3);
            AssertTransactions(transactions, expectedTransactions);
        }

        private void AssertTransactions(List<Transaction> transactionsToCheck, List<Transaction> expectedTransactions)
        {
            for(int i = 0; i < transactionsToCheck.Count; i++)
            {
                Transaction toCheck = transactionsToCheck[i];
                Transaction expected = expectedTransactions[i];
                
                toCheck.Date.Should().Be(expected.Date);
                toCheck.SizeLetter.Should().Be(expected.SizeLetter);
                toCheck.CarrierCode.Should().Be(expected.CarrierCode);
                toCheck.ShipmentPrice.Should().Be(expected.ShipmentPrice);
                toCheck.Discount.Should().Be(expected.Discount);
                toCheck.Valid.Should().Be(expected.Valid);
                toCheck.FailedTransaction.Should().Be(expected.FailedTransaction);
            }
        }

        private void SetupInputFileServiceMockForOnlyInvalidData()
        {
            _inputFileServiceMock.Setup(its => its.ReadInputAsync())
                .ReturnsAsync(new string[]
                {
                    "Labas",
                    "Krabas",
                    "Rabarbarai su cukrum"
                });
        }

        private void SetupInputFileServiceMock()
        {
            _inputFileServiceMock.Setup(its => its.ReadInputAsync())
                .ReturnsAsync(new string[]
                {
                    "2015-02-01 S MR",
                    "2015-02-02 S MR",
                    "2015-02-03 L LP",
                    "2015-02-05 S LP",
                    "2015-02-06 S MR",
                    "2015-02-06 L LP",
                    "2015-02-07 L MR",
                    "2015-02-08 M MR",
                    "2015-02-09 L LP",
                    "2015-02-10 L LP",
                    "2015-02-10 S MR",
                    "2015-02-10 S MR",
                    "2015-02-11 L LP",
                    "2015-02-12 M MR",
                    "2015-02-13 M LP",
                    "2015-02-15 S MR",
                    "2015-02-17 L LP",
                    "2015-02-17 S MR",
                    "2015-02-24 L LP",
                    "2015-02-29 CUSPS",
                    "2015-03-01 S MR"
                });

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
        }
    }
}
