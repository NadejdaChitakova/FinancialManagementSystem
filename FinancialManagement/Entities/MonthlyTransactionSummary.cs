namespace FinancialManagement.Entities
{
    public class MonthlyTransactionSummary
    {
        public string CategoryName { get; set; }
        public string BankName { get; set; }
        public int TotalTransactions { get; set; }
        public double TotalAmount { get; set; }
    }
}
