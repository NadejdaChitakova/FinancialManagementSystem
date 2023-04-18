using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinancialManagement.DbManagement;
using FinancialManagement.Entities;
using FinancialManagement.IRepositories;
using FinancialManagement.Models;
using Microsoft.EntityFrameworkCore;

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

        public List<TransactionResource> GetTransactions()
        {
            var transactions = _dbContext.Transactions.ProjectTo<TransactionResource>(_mapper.ConfigurationProvider).ToList();

            return transactions;
        }

        public PersonTransactionsResource GetTransactionByPerson(int personId)
        {
            var person = _dbContext.People.Where(x => x.PersonId.Equals(personId)).FirstOrDefault();

            if (person == null)
            {
                throw new BadHttpRequestException($"Person with {personId} does not exists.");
            }
            var mappedResource = _mapper.Map<PersonTransactionsResource>(person);

            mappedResource.Transactions.AddRange(_dbContext.Transactions.Where(x => x.PersonId.Equals(personId)).ProjectTo<TransactionResource>(_mapper.ConfigurationProvider).ToList());

            return mappedResource;
        }

        public TransactionsByLocationResource GetTransactionsByLocation(int locationId)
        {
            var location = _dbContext.Locations.Where(x => x.LocationId.Equals(locationId)).FirstOrDefault();

            if (location == null)
            {
                throw new BadHttpRequestException($"Location with {locationId} does not exists.");
            }

            var mappedResource = _mapper.Map<TransactionsByLocationResource>(location);

            mappedResource.Transactions.AddRange(_dbContext.Transactions.Where(x => x.LocationId.Equals(locationId)).ProjectTo<TransactionResource>(_mapper.ConfigurationProvider));

            return mappedResource;
        }

        public List<TransactionsByBank> GetTransactionByBank()
        {
            var transactions = _dbContext.Transactions.Include(x => x.Bank).Select(t => new TransactionsByBank
            {
                BankId = t.BankId,
                BankName = t.Bank.BankName,
                BankAddress = t.Bank.BankAddress,
                BankPhone = t.Bank.BankPhone,
                LocationId = t.LocationId,
                CategoryId = t.CategoryId,
                PersonId = t.PersonId,
                TransactionAmount = t.TransactionAmount,
                TransactionDate = t.TransactionDate,
                TransactionId = t.TransactionId,
                TransactionType = (int)t.TransactionType
            });
            return transactions.ToList();
        }

        public double? GetBalance(int personId)
        {
            var balance = _dbContext.Accounts.Where(x => x.PersonId.Equals(personId)).Select(x => x.Balance).FirstOrDefault();
            return balance;
        }

        public List<TransactionResource> GetTransactionsByDate(DateTime fromDate, DateTime toDate)
        {
            var transactions = _dbContext.Transactions.Where(x => x.TransactionDate >= fromDate && x.TransactionDate <= toDate).ProjectTo<TransactionResource>(_mapper.ConfigurationProvider).ToList();

            return transactions;
        }

        public List<TransactionsByCategory> GetTransactionsByCategories()
        {
            var transactions = _dbContext.Categories.ProjectTo<TransactionsByCategory>(_mapper.ConfigurationProvider).ToList();
            return transactions;
        }

        public List<TransactionResource> GetTransactionsByType(int typeId)
        {
            var transactions = _dbContext.Transactions.Where(x => x.TransactionType.Equals(typeId)).ProjectTo<TransactionResource>(_mapper.ConfigurationProvider).ToList();
            return transactions;
        }

        public List<MonthlyTransactionSummary> GetMonthlyTransactions(int month)
        {
            var transactions = _dbContext.Transactions
                                                      .Include(c => c.Category)
                                                      .Include(b => b.Bank)
                                                      .Where(x => x.TransactionDate.Month.Equals(month))
                                                      .GroupBy(cb => new { cb.Category.CategoryName, cb.Bank.BankName })
                                                      .Select(result => new MonthlyTransactionSummary
                                                      {
                                                          CategoryName = result.Key.CategoryName,
                                                          BankName = result.Key.BankName,
                                                          TotalTransactions = result.Count(),
                                                          TotalAmount = result.Sum(t => t.TransactionAmount)
                                                      })
                                                      .ToList();

            return transactions;
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
