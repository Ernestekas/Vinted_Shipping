using System;
using System.Collections.Generic;
using VintedShipping.Models;

namespace VintedShipping.Services
{
    public class TransactionService
    {
        private readonly InputFileService _inputFileService;

        public TransactionService(InputFileService inputFileService)
        {
            _inputFileService = inputFileService;
        }

        public void GetFullTransactions(string[] inputTransactions)
        {
            List<Transaction> transactions = new List<Transaction>();

            foreach(var t in inputTransactions)
            {
                transactions.Add(ParseTextToTransaction(t));
            }

            
        }

        private Transaction ParseTextToTransaction(string rawTransaction)
        {
            var parsedTransaction = new Transaction();
            string[] rawTransactionOptions = rawTransaction.Split(' ');

            if (string.IsNullOrWhiteSpace(rawTransaction))
            {
                return GetInvalidTransaction(rawTransaction);
            }

            if (rawTransactionOptions.Length != 3)
            {
                return GetInvalidTransaction(rawTransaction);
            }

            if (DateTime.TryParse(rawTransactionOptions[0], out DateTime date))
            {
                parsedTransaction.Date = date;
            }
            else
            {
                return GetInvalidTransaction(rawTransaction);
            }

            switch (rawTransactionOptions[1])
            {
                case "S":
                    parsedTransaction.SizeLetter = "S";
                    break;
                case "M":
                    parsedTransaction.SizeLetter = "M";
                    break;
                case "L":
                    parsedTransaction.SizeLetter = "L";
                    break;
                default:
                    return GetInvalidTransaction(rawTransaction);
            }

            parsedTransaction.CarrierCode = rawTransactionOptions[2];

            return parsedTransaction;
        }

        private Transaction GetInvalidTransaction(string rawTransaction)
        {
            return new Transaction()
            {
                FailedTransaction = rawTransaction + "Ignored",
                Valid = false
            };
        }
    }
}
