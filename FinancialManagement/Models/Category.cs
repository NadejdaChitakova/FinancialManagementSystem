namespace FinancialManagement.Models
{
    public partial class Category
    {
        public Category()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
