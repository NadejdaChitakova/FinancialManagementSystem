namespace FinancialManagement.Entities
{
    public class TransactionsByBank
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankPhone { get; set; }
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set; }
        public int TransactionType { get; set; }
        public int LocationId { get; set; }
        public int PersonId { get; set; }
        public int CategoryId { get; set; }
    }
}
