namespace FinancialManagement.Models
{
    public partial class Location
    {
        public Location()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int LocationId { get; set; }
        public string LocationName { get; set; } = null!;
        public string LocationAddress { get; set; } = null!;
        public double LocationLatitude { get; set; }
        public double LocationLongtude { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
