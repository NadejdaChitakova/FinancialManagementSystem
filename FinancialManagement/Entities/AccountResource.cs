namespace FinancialManagement.Entities
{
    public class AccountResource
    {
        public int PersonId { get; set; }
        public double? Balance { get; set; }
        public string Iban { get; set; } = null!;
        public string Currency { get; set; } = null!;
    }
}
