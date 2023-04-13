using FinancialManagement.Entities;
using FinancialManagement.Models;

namespace FinancialManagement.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionResource>> GetTransactions(int personId);
        Task<TransactionsByLocationResource> GetTransactionsByLocation(int locationId);
        Task CreateTransaction(TransactionRequestResource transactionResource);
        Task UpdateTransaction(TransactionRequestResource transactionResource);
        Task DeleteTransaction(int transactionId);
    }
}
