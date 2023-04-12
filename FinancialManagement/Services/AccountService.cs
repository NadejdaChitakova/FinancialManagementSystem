using AutoMapper;
using FinancialManagement.DbManagement;
using FinancialManagement.Entities;
using FinancialManagement.Interfaces;
using FinancialManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Services
{
    public class AccountService : IAccountService
    {
        private readonly FinancialManagementContext _financialManagementContext;
        private readonly IMapper _mapper;

        public AccountService(FinancialManagementContext financialManagementContext, IMapper mapper)
        {
            _financialManagementContext = financialManagementContext;
            _mapper = mapper;
        }

        public async Task CreateAccount(AccountResource request)
        {
            ValidateIBAN(request.Iban);

            var mappedResource = _mapper.Map<Account>(request);

            await _financialManagementContext.Accounts.AddAsync(mappedResource);
            await _financialManagementContext.SaveChangesAsync();
        }

        public async Task DeleteAccount(int personId)
        {
            if (personId < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }
            var account = _financialManagementContext.Accounts.AsQueryable().Where(x => x.PersonId.Equals(personId)).FirstOrDefault();

            if (account == null)
            {
                throw new BadHttpRequestException("Account does not exist");
            }

            _financialManagementContext.Accounts.Remove(account);
            await _financialManagementContext.SaveChangesAsync();
        }

        public async Task<AccountResource> GetAccount(int personId)
        {
            if (personId < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }

            var account = await _financialManagementContext.Accounts.Where(x => x.PersonId.Equals(personId)).FirstOrDefaultAsync();

            return _mapper.Map<AccountResource>(account);
        }

        public async Task UpdateAccount(AccountResource request)
        {
            if (request.PersonId < 0 || request.Balance < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }

            var mappedResource = _mapper.Map<Account>(request);

            _financialManagementContext.Accounts.Update(mappedResource);
            await _financialManagementContext.SaveChangesAsync();
        }

        private void ValidateIBAN(string iban)
        {
            var ibanFromDb = _financialManagementContext.Accounts.Where(x => x.Iban.Equals(iban, StringComparison.CurrentCultureIgnoreCase)).Select(x => x.Iban);

            if (ibanFromDb.Any())
            {
                throw new BadHttpRequestException("Iban value already exists in database.");
            }
        }
    }
}
