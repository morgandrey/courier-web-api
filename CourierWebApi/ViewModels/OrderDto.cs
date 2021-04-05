using System;
using System.Collections.Generic;

namespace CourierWebApi.ViewModels {
    public class OrderDto {
        public int OrderId { get; set; }
        public int? CourierId { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDescription { get; set; }
        public int OrderStatusId { get; set; }
        public decimal CourierReward { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}