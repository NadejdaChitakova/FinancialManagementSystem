
namespace FinancialManagement.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public int PersonId { get; set; }
        public double? Balance { get; set; }
        public string Iban { get; set; } = null!;
        public string Currency { get; set; } = null!;

        public virtual Person Person { get; set; } = null!;
    }
}
