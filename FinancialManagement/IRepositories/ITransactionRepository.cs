using FinancialManagement.Entities;
using FinancialManagement.Models;

namespace FinancialManagement.IRepositories
{
    public interface ITransactionRepository
    {
        List<TransactionResource> GetTransactionByPerson(int personId);
        TransactionsByLocationResource GetTransactionsByLocation(int locationId);
        double? GetBalance(int personId);
        Task AddTransactionToDb(Transaction transaction);
        Task UpdateTransactionToDb(Transaction transaction);
        Task DeleteTransactionToDb(Transaction transaction);
    }
}
