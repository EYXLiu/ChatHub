using System.ComponentModel.DataAnnotations;

namespace RESTChatHub.Models
{
    public class User
    {
        public string name { get; set; }
        [Key]
        public string ID { get; set; }

        public User() 
        {
            name = "Null";
            ID = "Null";
        }
        
        public User(string user, string id) 
        {
            name = user;
            ID = id;
        }
    }
}