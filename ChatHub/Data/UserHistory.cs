using Microsoft.EntityFrameworkCore;
using ChatHub.Models;

namespace ChatHub.Data
{
    public class UserHistory : DbContext
    {
        public UserHistory(DbContextOptions<UserHistory> options) : base(options) { }

        public DbSet<User> UserInfo { get; set; }
    }
}