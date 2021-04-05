#nullable disable

namespace CourierWebApi.Models
{
    public partial class ProductBranch
    {
        public int Id { get; set; }
        public int IdBranch { get; set; }
        public int IdProduct { get; set; }
        public bool IsAvailable { get; set; }
        public int ProductAmount { get; set; }

        public virtual Branch IdBranchNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
