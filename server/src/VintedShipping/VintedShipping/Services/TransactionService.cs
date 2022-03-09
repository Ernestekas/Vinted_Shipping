using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VintedShipping.Interfaces;
using VintedShipping.Models;

namespace VintedShipping.Services
{
    public class TransactionService : ITransactionService
    {
        private const decimal _maxTotalDiscountsAllowed = 10.00M;

        private int _largePackCountCurrentMonth = 0;
        private decimal _totalMonthlyDiscount = 0.00M;
        private int _discountMonth = 0;
        private int _maxLargePacksCountToDiscount = 1;
        private int _discountLargePackPerLargePacksCount = 3;
        private int _largePacksDiscounted = 0;

        private readonly IInputFileService _inputFileService;
        private readonly IProviderService _providerService;

        public TransactionService(IInputFileService inputFileService, IProviderService providerService)
        {
            _inputFileService = inputFileService;
            _providerService = providerService;
        }

        public async Task<List<Transaction>> GetTransactionsWithDiscounts()
        {
            List<Transaction> allTransactionsBases = await GetBaseData();
            List<Transaction> allTransactionsFull = await UpdateTransactionsInfo(allTransactionsBases);

            return allTransactionsFull;
        }

        private async Task<List<Transaction>> GetBaseData()
        {
            List<Transaction> resultTransactions = new List<Transaction>();

            string[] rawTransactionsData = await _inputFileService.ReadInputAsync();
            List<Provider> providers = await _providerService.GetProvidersAsync();

            foreach (var rawTransactionData in rawTransactionsData)
            {
                string[] rawTransactionDataContents = rawTransactionData.Split(' ');

                if (IsValid(rawTransactionDataContents, providers))
                {
                    resultTransactions.Add(GetValidTransaction(rawTransactionDataContents));
                    continue;
                }

                resultTransactions.Add(GetInvalidTransaction(rawTransactionData));
            }

            return resultTransactions;
        }

        private async Task<List<Transaction>> UpdateTransactionsInfo(List<Transaction> transactions)
        {
            List<Transaction> updatedTransactions = new List<Transaction>();

            _discountMonth = transactions.Select(t => t.Date.Month).Min();

            List<Provider> providers = await _providerService.GetProvidersAsync();
            List<Package> packages = providers.SelectMany(p => p.Packages).ToList();

            foreach (Transaction transaction in transactions)
            {
                if (!transaction.Valid)
                {
                    updatedTransactions.Add(transaction);
                    continue;
                }

                if (!MonthsMatches(_discountMonth, transaction.Date.Month))
                {
                    _totalMonthlyDiscount = 0.00M;
                    _discountMonth = transaction.Date.Month;
                    _largePackCountCurrentMonth = 0;
                }

                if (_totalMonthlyDiscount >= _maxTotalDiscountsAllowed)
                {
                    updatedTransactions.Add(GetTransactionInfoWithoutDiscount(transaction, providers));
                    continue;
                }

                updatedTransactions.Add(GetTransactionInfoWithDiscount(transaction, providers));
            }

            return updatedTransactions;
        }

        private Transaction GetTransactionInfoWithoutDiscount(Transaction transaction, List<Provider> providers)
        {
            transaction.ShipmentPrice = providers.FirstOrDefault(p => p.Code == transaction.CarrierCode).Packages.FirstOrDefault(pa => pa.SizeAbbreviation == transaction.SizeLetter).BasePrice;
            transaction.Discount = 0.00M;

            return transaction;
        }

        private Transaction GetTransactionInfoWithDiscount(Transaction transaction, List<Provider> providers)
        {
            switch (transaction.SizeLetter)
            {
                case "S":
                    GetSmallPackDiscount(transaction, providers);
                    break;
                case "L":
                    if (transaction.CarrierCode == "LP")
                    {
                        _largePackCountCurrentMonth++;
                    }
                    GetLargePackDiscount(transaction, providers);
                    break;
                default:
                    GetDefaultTransaction(transaction, providers);
                    break;
            }

            return transaction;
        }

        private void GetDefaultTransaction(Transaction transaction, List<Provider> providers)
        {
            transaction.ShipmentPrice =
                providers
                    .FirstOrDefault(p => p.Code == transaction.CarrierCode)
                .Packages
                    .FirstOrDefault(pa => pa.SizeAbbreviation == transaction.SizeLetter).BasePrice;

            transaction.Discount = 0.00M;
        }

        private void GetSmallPackDiscount(Transaction transaction, List<Provider> providers)
        {
            decimal lowestPrice =
                providers
                    .SelectMany(p => p.Packages)
                    .Where(p => p.SizeAbbreviation == "S")
                    .Select(p => p.BasePrice)
                    .Min();

            transaction.ShipmentPrice =
                providers
                    .FirstOrDefault(p => p.Code == transaction.CarrierCode)
                .Packages
                    .FirstOrDefault(pa => pa.SizeAbbreviation == transaction.SizeLetter).BasePrice;

            decimal discount = transaction.ShipmentPrice - lowestPrice;

            if (_totalMonthlyDiscount + discount <= _maxTotalDiscountsAllowed)
            {
                transaction.ShipmentPrice = lowestPrice;
                transaction.Discount = discount;
            }
            else
            {
                decimal discountLeft = _maxTotalDiscountsAllowed - _totalMonthlyDiscount;
                transaction.Discount = discountLeft;
                transaction.ShipmentPrice -= discountLeft;
                _totalMonthlyDiscount += transaction.Discount;
            }
        }

        private void GetLargePackDiscount(Transaction transaction, List<Provider> providers)
        {
            transaction.ShipmentPrice =
                providers
                    .FirstOrDefault(p => p.Code == transaction.CarrierCode)
                .Packages
                    .FirstOrDefault(pk => pk.SizeAbbreviation == "L")
                .BasePrice;

            decimal discount = transaction.ShipmentPrice;
            decimal discountsAllowed = _maxTotalDiscountsAllowed - _totalMonthlyDiscount;

            if (discount > discountsAllowed)
            {
                discount = discountsAllowed;
            }

            if (_largePackCountCurrentMonth % _discountLargePackPerLargePacksCount == 0 && _largePacksDiscounted < _maxLargePacksCountToDiscount && transaction.CarrierCode == "LP")
            {
                _largePacksDiscounted++;
                transaction.Discount = transaction.ShipmentPrice;
                transaction.ShipmentPrice -= transaction.Discount;
                _totalMonthlyDiscount += transaction.Discount;
            }
        }

        private bool MonthsMatches(int discountMonth, int transactionMonth)
        {
            return discountMonth == transactionMonth;
        }

        private Transaction GetInvalidTransaction(string rawTransactionData)
        {
            return new Transaction
            {
                FailedTransaction = rawTransactionData + " Ignored",
                Valid = false
            };
        }

        private Transaction GetValidTransaction(string[] rawTransactionDataContents)
        {
            return new Transaction
            {
                Date = DateTime.Parse(rawTransactionDataContents[0]),
                SizeLetter = rawTransactionDataContents[1],
                CarrierCode = rawTransactionDataContents[2],
                Valid = true
            };
        }

        private bool IsValid(string[] rawTransactionDataContents, List<Provider> providers)
        {
            return
                rawTransactionDataContents != null
                && rawTransactionDataContents.Length == 3
                && DateTime.TryParse(rawTransactionDataContents[0], out DateTime d)
                && providers.SelectMany(p => p.Packages).Select(s => s.SizeAbbreviation).Contains(rawTransactionDataContents[1])
                && providers.Select(p => p.Code).Contains(rawTransactionDataContents[2]);
        }
    }
}
