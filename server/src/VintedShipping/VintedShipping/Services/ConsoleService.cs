using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VintedShipping.Models;

namespace VintedShipping.Services
{
    public class ConsoleService
    {
        private readonly InputFileService _inputFileService;
        private readonly TransactionService _transactionService;

        public ConsoleService(InputFileService inputFileService, TransactionService transactionService)
        {
            _inputFileService = inputFileService;
            _transactionService = transactionService;
        }

        public async Task Run()
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Select action number: ");
                Console.WriteLine("1 - Get shipment discounts.");
                Console.WriteLine("2 - Exit an application");
                Console.WriteLine();
                string action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        List<Transaction> transactions = await _transactionService.GetTransactionsWithDiscounts();
                        PrintTransactions(transactions);
                        Console.ReadKey();
                        break;
                    case "2":
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }

        private void PrintTransactions(List<Transaction> transactions)
        {
            foreach (Transaction transaction in transactions)
            {
                if (!transaction.Valid)
                {
                    Console.WriteLine(transaction.FailedTransaction);
                }
                else
                {
                    string outputTransaction =
                        $"{transaction.Date.ToShortDateString()} " +
                        $"{transaction.SizeLetter} " +
                        $"{transaction.CarrierCode} " +
                        $"{transaction.ShipmentPrice}";

                    if(transaction.Discount == 0)
                    {
                        outputTransaction += " -";
                    }
                    else
                    {
                        outputTransaction += $" {transaction.Discount}";
                    }

                    Console.WriteLine(outputTransaction);
                }
            }
        }
    }
}
