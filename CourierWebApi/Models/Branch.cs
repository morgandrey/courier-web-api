using System.Collections.Generic;

#nullable disable

namespace CourierWebApi.Models
{
    public partial class Branch
    {
        public Branch()
        {
            ProductBranches = new HashSet<ProductBranch>();
        }

        public int IdBranch { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }

        public virtual ICollection<ProductBranch> ProductBranches { get; set; }
    }
}
