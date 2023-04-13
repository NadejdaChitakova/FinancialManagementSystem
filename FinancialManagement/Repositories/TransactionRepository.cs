using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinancialManagement.DbManagement;
using FinancialManagement.Entities;
using FinancialManagement.IRepositories;
using FinancialManagement.Models;

namespace FinancialManagement.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FinancialManagementContext _dbContext;
        private readonly IMapper _mapper;

        public TransactionRepository(FinancialManagementContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<TransactionResource> GetTransactionByPerson(int personId)
        {
            var transactions = _dbContext.Transactions.Where(x => x.PersonId.Equals(personId)).ProjectTo<TransactionResource>(_mapper.ConfigurationProvider).ToList();

            return transactions;
        }

        public TransactionsByLocationResource GetTransactionsByLocation(int locationId)
        {
            var location = _dbContext.Locations.Where(x => x.LocationId.Equals(locationId)).FirstOrDefault();

            var mappedResource = _mapper.Map<TransactionsByLocationResource>(location);

            mappedResource.Transactions.AddRange(_dbContext.Transactions.Where(x => x.LocationId.Equals(locationId)).ProjectTo<TransactionResource>(_mapper.ConfigurationProvider));

            return mappedResource;
        }

        public double? GetBalance(int personId)
        {
            var balance = _dbContext.Accounts.Where(x => x.PersonId.Equals(personId)).Select(x => x.Balance).FirstOrDefault();
            return balance;
        }

        public async Task AddTransactionToDb(Transaction transaction)
        {
            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTransactionToDb(Transaction transaction)
        {
            _dbContext.Transactions.Update(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTransactionToDb(Transaction transaction)
        {
            _dbContext.Transactions.Remove(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public Transaction? GetTransaction(int transactionId)
        {
            return _dbContext.Transactions.Where(x => x.TransactionId.Equals(x.TransactionId)).FirstOrDefault();
        }
    }
}
