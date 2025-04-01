using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using HotChocolate.Subscriptions;

namespace GraphQLChatHub.GraphQL
{
    public class Subscription
    {
        public async IAsyncEnumerable<Message> OnSendMessage([Service] ITopicEventReceiver eventReceiver, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var sourceStream = await eventReceiver.SubscribeAsync<Message>(nameof(Mutation.SendMessage), cancellationToken);
            yield return new Message { Content = "First Message", Sender = new User { Email = "service@gmail.com", Username="Service" }};

            await Task.Delay(5000);

            await foreach (Message m in sourceStream.ReadEventsAsync())
            {
                yield return m;
            }
        }
        [Subscribe(With = nameof(OnSendMessage))]
        public Message SendMessage([Topic] string channel, [EventMessage] Message m) => m;
    }
}