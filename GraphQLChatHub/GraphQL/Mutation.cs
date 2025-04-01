using HotChocolate;
using HotChocolate.Subscriptions;

namespace GraphQLChatHub.GraphQL
{
    public class Mutation
    {
        public async Task<Message> SendMessage(User sender, string message, [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
            {
                var m = new Message { Content = message, Sender = new User{ Username=sender.Username, Email=sender.Email }};
                await eventSender.SendAsync(nameof(SendMessage), m, cancellationToken);
                return m;
            }
    }
}