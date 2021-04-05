using System.Collections.Generic;

#nullable disable

namespace CourierWebApi.Models
{
    public partial class Client
    {
        public Client()
        {
            CourierRatings = new HashSet<CourierRating>();
            Orders = new HashSet<Order>();
        }

        public int IdClient { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPassword { get; set; }
        public string ClientSalt { get; set; }
        public string ClientPatronymic { get; set; }
        public decimal ClientRating { get; set; }

        public virtual ICollection<CourierRating> CourierRatings { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
