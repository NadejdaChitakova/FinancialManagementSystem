using AutoMapper;
using FinancialManagement.DbManagement;
using FinancialManagement.Entities;
using FinancialManagement.Interfaces;
using FinancialManagement.IRepositories;
using FinancialManagement.Models;

namespace FinancialManagement.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;

        public AccountService(IMapper mapper, IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public async Task<AccountResource> GetAccount(int personId)
        {
            if (personId < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }

            var account = _accountRepository.GetAccount(personId);

            return _mapper.Map<AccountResource>(account);
        }

        public async Task CreateAccount(AccountResource request)
        {
            ValidateIBAN(request.Iban);

            var account = _mapper.Map<Account>(request);

            await _accountRepository.CreateAccountInDb(account);
        }

        public async Task UpdateAccount(AccountResource request)
        {
            if (request.PersonId < 0 || request.Balance < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }

            var account = _mapper.Map<Account>(request);

            await _accountRepository.UpdateAccountInDb(account);
        }

        public async Task DeleteAccount(int accountId)
        {
            if (accountId < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }
            var account = _accountRepository.GetAccount(accountId);

            if (account == null)
            {
                throw new BadHttpRequestException("Account does not exist");
            }

            await _accountRepository.DeleteAccountInDb(account);
        }

        private void ValidateIBAN(string iban)
        {
            var ibanFromDb = _accountRepository.GetIban(iban);

            if (!string.IsNullOrEmpty(ibanFromDb))
            {
                throw new BadHttpRequestException("Iban value already exists in database.");
            }
        }
    }
}
