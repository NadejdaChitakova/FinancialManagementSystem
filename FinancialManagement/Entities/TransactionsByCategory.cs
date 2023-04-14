namespace FinancialManagement.Entities
{
    public class TransactionsByCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public List<TransactionResource> Transactions { get; set; }
    }
}
