namespace FinancialManagement.Entities
{
    public class PersonTransactionsResource
    {
        public int PersonId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<TransactionResource> Transactions { get; set; }
    }
}
