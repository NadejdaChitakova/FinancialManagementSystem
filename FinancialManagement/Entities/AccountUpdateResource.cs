namespace FinancialManagement.Entities
{
    public class AccountUpdateResource
    {
        public int AccountId { get; set; }
        public double? Balance { get; set; }
        public string Iban { get; set; } = null!;
        public string Currency { get; set; } = null!;
    }
}
