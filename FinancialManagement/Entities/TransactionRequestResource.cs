namespace FinancialManagement.Entities
{
    public class TransactionRequestResource
    {
        public double TransactionAmount { get; set; }
        public int TransactionType { get; set; }
        public int BankId { get; set; }
        public int LocationId { get; set; }
        public int PersonId { get; set; }
        public int CategoryId { get; set; }
    }
}
