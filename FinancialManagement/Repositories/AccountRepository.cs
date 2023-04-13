using AutoMapper;
using FinancialManagement.DbManagement;
using FinancialManagement.IRepositories;
using FinancialManagement.Models;
using System.Runtime.CompilerServices;

namespace FinancialManagement.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FinancialManagementContext _financialManagementContext;
        private readonly IMapper _mapper;

        public AccountRepository(FinancialManagementContext financialManagementContext, IMapper mapper)
        {
            _financialManagementContext = financialManagementContext;
            _mapper = mapper;
        }

        public Account? GetAccount(int accountId)
        {
            return _financialManagementContext.Accounts.Where(x => x.AccountId.Equals(accountId)).FirstOrDefault();
        }

        public string? GetIban(string iban)
        {
            return _financialManagementContext.Accounts.Where(x => x.Iban.ToLower().Equals(iban.ToLower())).Select(x => x.Iban).FirstOrDefault();
        }

        public async Task CreateAccountInDb(Account account)
        {
            await _financialManagementContext.Accounts.AddAsync(account);
            await _financialManagementContext.SaveChangesAsync();
        }

        public async Task UpdateAccountInDb(Account account)
        {
            account.AccountId = GetAccountId(account.Iban);

            _financialManagementContext.Accounts.Update(account);
            await _financialManagementContext.SaveChangesAsync();
        }

        public async Task DeleteAccountInDb(Account account)
        {
            _financialManagementContext.Accounts.Remove(account);
            await _financialManagementContext.SaveChangesAsync();
        }

        private int GetAccountId(string ibna)
        {
            return _financialManagementContext.Accounts.Where(x => x.Iban.ToLower() == ibna.ToLower()).Select(x => x.AccountId).FirstOrDefault();
        }
    }
}
