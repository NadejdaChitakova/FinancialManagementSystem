namespace FinancialManagement.Models
{
    public partial class Bank
    {
        public Bank()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int BankId { get; set; }
        public string BankName { get; set; } = null!;
        public string BankAddress { get; set; } = null!;
        public string BankPhone { get; set; } = null!;

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
