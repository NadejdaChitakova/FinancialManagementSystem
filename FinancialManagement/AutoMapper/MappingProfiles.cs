﻿using AutoMapper;
using FinancialManagement.Entities;
using FinancialManagement.Models;
using System.Net;

namespace FinancialManagement.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Transaction, TransactionResource>();
            CreateMap<TransactionRequestResource, Transaction>()
                .ForMember(
                    dest => dest.TransactionDate,
                    opt => opt.MapFrom(src => DateTime.Now)
                );
            CreateMap<TransactionsByLocationResource, Location>();
            CreateMap<Location, TransactionsByLocationResource>();
            CreateMap<TransactionsByCategory, Category>();
            CreateMap<Category, TransactionsByCategory > ();

            CreateMap<Account, AccountResource>();
            CreateMap<AccountResource, Account>();

            CreateMap<PersonTransactionsResource, Person>();
            CreateMap<Person, PersonTransactionsResource>();
        }
    }
}
