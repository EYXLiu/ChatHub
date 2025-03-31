using System.ComponentModel.DataAnnotations;

namespace ChatHub.Models
{
    public class ChatMessage
    {
        [Key]
        public string ID { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public ChatMessage() 
        {
            ID = "Null";
            Message = "Null";
        }
        
        public ChatMessage(string id, string sender, string message) 
        {
            ID = id;
            Message = message;
        }
    }
}