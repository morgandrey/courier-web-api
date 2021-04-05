using System.Collections.Generic;

#nullable disable

namespace CourierWebApi.Models
{
    public partial class Courier
    {
        public Courier()
        {
            CarCouriers = new HashSet<CarCourier>();
            CourierRatings = new HashSet<CourierRating>();
            Orders = new HashSet<Order>();
        }

        public int IdCourier { get; set; }
        public string CourierName { get; set; }
        public string CourierSurname { get; set; }
        public string CourierPhone { get; set; }
        public string CourierImage { get; set; }
        public string CourierPassword { get; set; }
        public string CourierSalt { get; set; }
        public decimal CourierMoney { get; set; }
        public string CourierPatronymic { get; set; }
        public decimal CourierRating { get; set; }

        public virtual ICollection<CarCourier> CarCouriers { get; set; }
        public virtual ICollection<CourierRating> CourierRatings { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
