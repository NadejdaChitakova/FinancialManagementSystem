﻿namespace FinancialManagement.Entities
{
    public class TransactionResource
    {
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set; }
        public string TransactionType { get; set; } = null!;
        public int BankId { get; set; }
        public int LocationId { get; set; }
        public int PersonId { get; set; }
        public int CategoryId { get; set; }
    }
}