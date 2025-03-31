using System.Net.WebSockets;
using System.Text;
using System.Collections.Concurrent;
using ChatHub.Data;
using ChatHub.Models;

namespace ChatHub.Services
{
    public class WebSocketConnectionManager
    {
        private readonly ConcurrentDictionary<string, (string, WebSocket)> _sockets = new();

        private readonly IServiceProvider _serviceProvider;

        private async Task AddChatHistory(ChatMessage chatMessage)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var chatHistory = scope.ServiceProvider.GetRequiredService<ChatHistory>();
                chatHistory.ChatMessages.Add(chatMessage);
                await chatHistory.SaveChangesAsync();
            }
        }

        private async Task AddUserHistory(User user)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                UserHistory userHistory = scope.ServiceProvider.GetRequiredService<UserHistory>();
                userHistory.UserInfo.Add(user);
                await userHistory.SaveChangesAsync();
            }
        }

        public WebSocketConnectionManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task AddSocket(string id, string name, WebSocket socket)
        {
            _sockets.TryAdd(id, (name, socket));
            await BroadcastMessage($"Welcome user {name}");
            var user = new User(name, id);
            await AddUserHistory(user);
        }

        public async Task RemoveSocket(string id) {
            if (_sockets.TryRemove(id, out var socket))
            {
                await BroadcastMessage($"{socket.Item1} has disconnected");
                await socket.Item2.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
        }

        public async Task BroadcastMessage(string message, string? socketName = null, string? socketID = null)
        {
            if (socketName != null && socketID != null)
            {
                var messageID = Guid.NewGuid().ToString();
                var chatMessage = new ChatMessage(messageID, socketName, message.Replace(socketName + ": ", String.Empty));
                await AddChatHistory(chatMessage);
            }

            var bytes = Encoding.UTF8.GetBytes(message);
            foreach (var (ID, (name, socket)) in _sockets)
            {
                if (socket.State == WebSocketState.Open && (socketID == null || socketID != ID))
                {
                    await socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}