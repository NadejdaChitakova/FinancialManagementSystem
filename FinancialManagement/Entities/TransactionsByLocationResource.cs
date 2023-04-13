using FinancialManagement.Models;

namespace FinancialManagement.Entities
{
    public class TransactionsByLocationResource
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; } = null!;
        public string LocationAddress { get; set; } = null!;
        public double LocationLatitude { get; set; }
        public double LocationLongtude { get; set; }
        public List<TransactionResource> Transactions { get; set; }
    }
}
