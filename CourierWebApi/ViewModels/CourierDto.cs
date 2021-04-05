namespace CourierWebApi.ViewModels
{
    public class CourierDto
    {
        public int IdCourier { get; set; }
        public string CourierName { get; set; }
        public string CourierSurname { get; set; }
        public string CourierPatronymic { get; set; }
        public string CourierPhone { get; set; }
        public string CourierImage { get; set; }
        public string CourierPassword { get; set; }
        public string CourierSalt { get; set; }
        public decimal CourierMoney { get; set; }
        public decimal CourierRating { get; set; }
        public int CourierRatingCount { get; set; }
    }
}