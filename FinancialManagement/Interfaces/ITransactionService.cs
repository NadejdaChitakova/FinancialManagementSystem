using FinancialManagement.Entities;
using FinancialManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagement.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionResource>> GetTransactions(int personId);
        Task CreateTransaction(TransactionRequestResource transactionResource);
        Task UpdateTransaction(TransactionRequestResource transactionResource);
        Task DeleteTransaction(int transactionId);
    }
}
