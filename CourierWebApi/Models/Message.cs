#nullable disable

namespace CourierWebApi.Models
{
    public partial class Message
    {
        public int IdMessage { get; set; }
        public string MessageDate { get; set; }
        public string MessageInput { get; set; }
        public int IdOrder { get; set; }
        public string MessageFrom { get; set; }
        public string MessageTo { get; set; }
        public int? IdChatClient { get; set; }

        public virtual Order IdOrderNavigation { get; set; }
    }
}
