using System.Collections.Generic;

#nullable disable

namespace CourierWebApi.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductBranches = new HashSet<ProductBranch>();
            ProductOrders = new HashSet<ProductOrder>();
        }

        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }

        public virtual ICollection<ProductBranch> ProductBranches { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
