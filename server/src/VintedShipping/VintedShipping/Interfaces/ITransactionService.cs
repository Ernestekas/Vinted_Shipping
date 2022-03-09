using System.Collections.Generic;
using System.Threading.Tasks;
using VintedShipping.Models;

namespace VintedShipping.Interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetTransactionsWithDiscounts();
    }
}