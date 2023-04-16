﻿using AutoMapper;
using FinancialManagement.Entities;
using FinancialManagement.Interfaces;
using FinancialManagement.IRepositories;
using FinancialManagement.Models;

namespace FinancialManagement.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IMapper mapper, ITransactionRepository transactionRepository)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
        }

        public async Task<List<TransactionResource>> GetTransactions()
        {
            return _transactionRepository.GetTransactions();
        }

        public async Task<PersonTransactionsResource> GetTransactionsByPerson(int personId)
        {
            if (personId < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }
            return _transactionRepository.GetTransactionByPerson(personId);
        }

        public async Task<TransactionsByLocationResource> GetTransactionsByLocation(int locationId)
        {
            if (locationId < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }

            return _transactionRepository.GetTransactionsByLocation(locationId);
        }

        public async Task CreateTransaction(TransactionRequestResource transactionResource)
        {
            if (transactionResource.PersonId < 0 || transactionResource.TransactionAmount < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }

            var personBalance = _transactionRepository.GetBalance(transactionResource.PersonId);

            if (!personBalance.HasValue)
            {
                throw new BadHttpRequestException($"The person with id {transactionResource.PersonId} does not have an account.");
            }

            if (transactionResource.TransactionAmount >= personBalance)
            {
                throw new BadHttpRequestException($"The sum of transactions is bigger that amount in account.");
            }

            var transaction = _mapper.Map<Transaction>(transactionResource);

            await _transactionRepository.AddTransactionToDb(transaction);
        }

        public async Task<List<TransactionsByBank>> GetTransactionsByBank()
        {
            return _transactionRepository.GetTransactionByBank();
        }

        public async Task<List<TransactionResource>> GetTransactionsByDate(DateTime fromDate, DateTime toDate)
        {
            if (fromDate > toDate)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }

            return _transactionRepository.GetTransactionsByDate(fromDate, toDate);
        }

        public async Task<List<TransactionsByCategory>> GetTransactionsByCategories()
        {
            return _transactionRepository.GetTransactionsByCategories();
        }

        public async Task<List<TransactionResource>> GetTransactionsByType(int typeId)
        {
            var transactions = _transactionRepository.GetTransactionsByType(typeId);

            if (!transactions.Any())
            {
                throw new BadHttpRequestException($"Missing data for type with id {typeId}");
            }

            return transactions;
        }

        public async Task UpdateTransaction(TransactionRequestResource transactionResource)
        {
            if (transactionResource.PersonId < 0 || transactionResource.TransactionAmount < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }

            var transaction = _mapper.Map<Transaction>(transactionResource);

            await _transactionRepository.UpdateTransactionToDb(transaction);
        }

        public async Task DeleteTransaction(int transactionId)
        {
            if (transactionId < 0)
            {
                throw new BadHttpRequestException("Incorrect data format.");
            }

            var transaction = _transactionRepository.GetTransaction(transactionId);

            if (transaction == null)
            {
                throw new BadHttpRequestException("Transaction does not exist");
            }

            await _transactionRepository.DeleteTransactionToDb(transaction);
        }
    }
}
