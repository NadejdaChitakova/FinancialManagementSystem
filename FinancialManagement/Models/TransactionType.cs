using System;
using System.Collections.Generic;

namespace FinancialManagement.Models
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int TypeId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
