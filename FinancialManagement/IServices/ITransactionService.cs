using FinancialManagement.Entities;
using FinancialManagement.Models;

namespace FinancialManagement.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionResource>> GetTransactions();
        Task<PersonTransactionsResource> GetTransactionsByPerson(int personId);
        Task<TransactionsByLocationResource> GetTransactionsByLocation(int locationId);
        Task<List<TransactionsByBank>> GetTransactionsByBank();
        Task<List<TransactionResource>> GetTransactionsByDate(DateTime fromDate, DateTime toDate);
        Task<List<TransactionsByCategory>> GetTransactionsByCategories();
        Task CreateTransaction(TransactionRequestResource transactionResource);
        Task UpdateTransaction(TransactionRequestResource transactionResource);
        Task DeleteTransaction(int transactionId);
    }
}
