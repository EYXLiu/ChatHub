using Microsoft.EntityFrameworkCore;
using RESTChatHub.Models;

namespace RESTChatHub.Data
{
    public class ChatHistory : DbContext
    {
        public ChatHistory(DbContextOptions<ChatHistory> options) : base(options) { }

        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}