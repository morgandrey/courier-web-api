using System.Collections.Generic;

#nullable disable

namespace CourierWebApi.Models
{
    public partial class Car
    {
        public Car()
        {
            CarCouriers = new HashSet<CarCourier>();
        }

        public int IdCar { get; set; }
        public string CarName { get; set; }

        public virtual ICollection<CarCourier> CarCouriers { get; set; }
    }
}
