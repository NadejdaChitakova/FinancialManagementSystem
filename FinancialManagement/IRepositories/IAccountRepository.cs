using FinancialManagement.Models;

namespace FinancialManagement.IRepositories
{
    public interface IAccountRepository
    {
        Account? GetAccount(int accountId);
        string? GetIban(string iban);
        Task CreateAccountInDb(Account account);
        Task UpdateAccountInDb(Account account);
        Task DeleteAccountInDb(Account account);

    }
}
