using Microsoft.EntityFrameworkCore;
using RESTChatHub.Models;

namespace RESTChatHub.Data
{
    public class UserHistory : DbContext
    {
        public UserHistory(DbContextOptions<UserHistory> options) : base(options) { }

        public DbSet<User> UserInfo { get; set; }
    }
}