using System;
using System.Collections.Generic;

#nullable disable

namespace CourierWebApi.Models
{
    public partial class Order
    {
        public Order()
        {
            Messages = new HashSet<Message>();
            ProductOrders = new HashSet<ProductOrder>();
        }

        public int IdOrder { get; set; }
        public int IdClient { get; set; }
        public int? IdCourier { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDescription { get; set; }
        public int IdOrderStatus { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal CourierReward { get; set; }

        public virtual Client IdClientNavigation { get; set; }
        public virtual Courier IdCourierNavigation { get; set; }
        public virtual OrderStatus IdOrderStatusNavigation { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
