#nullable disable

namespace CourierWebApi.Models
{
    public partial class CourierRating
    {
        public int IdCourierRating { get; set; }
        public int IdCourier { get; set; }
        public int IdClient { get; set; }
        public int IdRating { get; set; }
        public bool IsRatedByCourier { get; set; }
        public string RatingComment { get; set; }

        public virtual Client IdClientNavigation { get; set; }
        public virtual Courier IdCourierNavigation { get; set; }
        public virtual Rating IdRatingNavigation { get; set; }
    }
}
