namespace GraphQLChatHub.GraphQL
{
    public class Query
    {
        public Message GetMessage() =>
            new Message
            {
                Content = "Hello User",
                Sender = new User { Username = "Server", Email="" }
            };
    }
}