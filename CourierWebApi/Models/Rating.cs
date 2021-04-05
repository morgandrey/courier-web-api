using System.Collections.Generic;

#nullable disable

namespace CourierWebApi.Models
{
    public partial class Rating
    {
        public Rating()
        {
            CourierRatings = new HashSet<CourierRating>();
        }

        public int IdRating { get; set; }
        public string RatingDescription { get; set; }
        public int RatingScore { get; set; }

        public virtual ICollection<CourierRating> CourierRatings { get; set; }
    }
}
