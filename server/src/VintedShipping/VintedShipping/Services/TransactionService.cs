using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<string> outputTransactions = new List<string>();

            foreach(var transaction in inputTransactions)
            {
                List<string> transactionDetails = transaction.Split(' ').ToList();
                
                if(transactionDetails == null)
                {
                    outputTransactions.Add("Ignored");
                }

                if(transactionDetails.Count != 4)
                {
                    outputTransactions.Add(outputTransactions + " Ignored");
                }



                //if(!DateTime.TryParse(transactionDetails[0], out DateTime dateTime))
                //{
                //    outputTransactions.Add($"{transactionDetails[0]} Ignored");
                //}

            }

        }
    }
}
