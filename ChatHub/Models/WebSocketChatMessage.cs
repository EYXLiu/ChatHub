namespace ChatHub.Models
{
    public class ChatMessage
    {
        public string ID { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public ChatMessage() 
        {
            ID = "Null";
            Sender = "Null";
            Message = "Null";
        }
        
        public ChatMessage(string id, string sender, string message) 
        {
            ID = id;
            Sender = sender;
            Message = message;
        }
    }
}