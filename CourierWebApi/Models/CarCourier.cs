#nullable disable

namespace CourierWebApi.Models
{
    public partial class CarCourier
    {
        public int Id { get; set; }
        public int IdCourier { get; set; }
        public int IdCar { get; set; }
        public string CarNumber { get; set; }

        public virtual Car IdCarNavigation { get; set; }
        public virtual Courier IdCourierNavigation { get; set; }
    }
}
