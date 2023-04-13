using FinancialManagement.Entities;

namespace FinancialManagement.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResource> GetAccount(int personId);
        Task CreateAccount(AccountResource request);
        Task UpdateAccount(AccountResource request);
        Task DeleteAccount(int personId);

    }
}
