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

        private readonly ChatHistory _chatContext;

        public WebSocketConnectionManager(ChatHistory chatContext)
        {
            _chatContext = chatContext;
        }

        public async void AddSocket(string id, string name, WebSocket socket)
        {
            _sockets.TryAdd(id, (name, socket));
            await BroadcastMessage($"Welcome user {name}");
        }

        public async Task RemoveSocket(string id) {
            if (_sockets.TryRemove(id, out var socket))
            {
                await socket.Item2.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
        }

        public async Task BroadcastMessage(string message, string? socketName = null, string? socketID = null)
        {
            if (socketName != null && socketID != null)
            {
                var chatMessage = new ChatMessage(socketID, socketName, message.Replace(socketName + ": ", String.Empty));
                _chatContext.ChatMessages.Add(chatMessage);
                await _chatContext.SaveChangesAsync();
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