using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinancialManagement.DbManagement;
using FinancialManagement.Entities;
using FinancialManagement.Interfaces;
using FinancialManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Diagnostics;

namespace FinancialManagement.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly FinancialManagementContext _financialManagementContext;
        private readonly IMapper _mapper;

        public TransactionService(FinancialManagementContext financialManagementContext, IMapper mapper)
        {
            _financialManagementContext = financialManagementContext;
            _mapper = mapper;
        }

        public async Task<List<TransactionResource>> GetTransactions(int personId)
        {
            if (personId < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }

            var transactions = _financialManagementContext.Transactions.Where(x => x.PersonId.Equals(personId)).ProjectTo<TransactionResource>(_mapper.ConfigurationProvider).ToList();
            return transactions;
        }

        public async Task<TransactionsByLocationResource> GetTransactionsByLocation(int locationId)
        {
            if (locationId < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }

            var location = _financialManagementContext.Locations.Where(x => x.LocationId.Equals(locationId)).FirstOrDefault();
            var mappedResource = _mapper.Map<TransactionsByLocationResource>(location);
            mappedResource.Transactions.AddRange(_financialManagementContext.Transactions.Where(x => x.LocationId.Equals(locationId)));
            return mappedResource;
        }

        public async Task CreateTransaction(TransactionRequestResource transactionResource)
        {
            if (transactionResource.PersonId < 0 || transactionResource.TransactionAmount < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }

            var personBalance = _financialManagementContext.Accounts.AsQueryable().Where(x => x.PersonId.Equals(transactionResource.PersonId)).Select(x => x.Balance).FirstOrDefault();

            if (!personBalance.HasValue)
            {
                throw new BadHttpRequestException($"The person with id {transactionResource.PersonId} does not have an account.");
            }

            if (transactionResource.TransactionAmount >= personBalance)
            {
                throw new BadHttpRequestException($"The sum of transactions is bigger that amount in account.");
            }

            var mappedResource = _mapper.Map<Transaction>(transactionResource);

            await _financialManagementContext.Transactions.AddAsync(mappedResource);
            await _financialManagementContext.SaveChangesAsync();
        }

        public async Task UpdateTransaction(TransactionRequestResource transactionResource)
        {
            if (transactionResource.PersonId < 0 || transactionResource.TransactionAmount < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }

            var mappedResource = _mapper.Map<Transaction>(transactionResource);

            _financialManagementContext.Transactions.Update(mappedResource);
            await _financialManagementContext.SaveChangesAsync();
        }

        public async Task DeleteTransaction(int transactionId)
        {
            if (transactionId < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }

            var transaction = _financialManagementContext.Transactions.AsQueryable().Where(x => x.TransactionId.Equals(x.TransactionId)).FirstOrDefault();

            if (transaction == null)
            {
                throw new BadHttpRequestException("Transaction does not exist");
            }

            _financialManagementContext.Transactions.Remove(transaction);
            await _financialManagementContext.SaveChangesAsync();
        }
    }
}
