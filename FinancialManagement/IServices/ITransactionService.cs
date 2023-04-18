using FinancialManagement.Entities;
using FinancialManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagement.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionResource>> GetTransactions();
        Task ExportTransactionsToPdf();
        Task<PersonTransactionsResource> GetTransactionsByPerson(int personId);
        Task<TransactionsByLocationResource> GetTransactionsByLocation(int locationId);
        Task<List<TransactionsByBank>> GetTransactionsByBank();
        Task<List<TransactionResource>> GetTransactionsByDate(DateTime fromDate, DateTime toDate);
        Task<List<TransactionsByCategory>> GetTransactionsByCategories();
        Task<List<TransactionResource>> GetTransactionsByType(int typeId);
        Task<List<MonthlyTransactionSummary>> GetMonthlyTransactionSummary(int month);
        Task CreateTransaction(TransactionRequestResource transactionResource);
        Task UpdateTransaction(TransactionRequestResource transactionResource);
        Task DeleteTransaction(int transactionId);
    }
}
