namespace FinancialManagement.Models
{
    public partial class Person
    {
        public Person()
        {
            Accounts = new HashSet<Account>();
            Transactions = new HashSet<Transaction>();
        }

        public int PersonId { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
