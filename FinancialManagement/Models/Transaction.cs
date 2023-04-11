namespace FinancialManagement.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set; }
        public string TransactionType { get; set; } = null!;
        public int BankId { get; set; }
        public int LocationId { get; set; }
        public int PersonId { get; set; }
        public int CategoryId { get; set; }

        public virtual Bank Bank { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual Location Location { get; set; } = null!;
        public virtual Person Person { get; set; } = null!;
    }
}
