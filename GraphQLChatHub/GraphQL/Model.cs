namespace GraphQLChatHub.GraphQL
{
    public class Message
    {
        public User? Sender { get; set; }
        public string? Content { get; set; }
    }

    public class User
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}