using FinancialManagement.Entities;
using FinancialManagement.Models;

namespace FinancialManagement.IRepositories
{
    public interface ITransactionRepository
    {
        Transaction? GetTransaction(int transactionId);
        List<TransactionResource> GetTransactions();
        PersonTransactionsResource GetTransactionByPerson(int personId);
        TransactionsByLocationResource GetTransactionsByLocation(int locationId);
        List<TransactionsByBank> GetTransactionByBank();
        double? GetBalance(int personId);
        Task AddTransactionToDb(Transaction transaction);
        Task UpdateTransactionToDb(Transaction transaction);
        Task DeleteTransactionToDb(Transaction transaction);
    }
}
