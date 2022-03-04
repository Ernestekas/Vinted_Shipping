using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VintedShipping.Models;

namespace VintedShipping.Services
{
    public class TransactionService
    {
        private readonly InputFileService _inputFileService;
        private readonly ProviderService _providerService;

        public TransactionService(InputFileService inputFileService, ProviderService providerService)
        {
            _inputFileService = inputFileService;
            _providerService = providerService;
        }

        public void GetFullTransactions(string[] inputTransactions)
        {
            List<Transaction> transactions = FormTransactions(inputTransactions);
            
            foreach(var transaction in transactions)
            {
                
            }
        }

        private async Task<List<Transaction>> FormTransactions(string[] inputTransactions)
        {
            var transactions = new List<Transaction>();
            foreach(var t in inputTransactions)
            {
                var transaction = await ParseTextToTransactionAsync(t); 
                transactions.Add(transaction);
            }

            return transactions;
        }

        private async Task<Transaction> ParseTextToTransactionAsync(string rawTransaction)
        {
            List<Provider> providers = await _providerService.GetProvidersAsync();

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
