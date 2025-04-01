using System.Net.WebSockets;
using System.Text;

namespace RESTChatHub.Services
{
    public class WebSocketHandler
    {
        private readonly WebSocketConnectionManager _manager;

        public WebSocketHandler(WebSocketConnectionManager manager) => _manager = manager;

        public async Task HandleConnectionAsync(WebSocket webSocket, string name = "Anonymous")
        {
            var socketID = Guid.NewGuid().ToString();
            await _manager.AddSocket(socketID, name, webSocket);

            var buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    await _manager.BroadcastMessage($"{name}: {message}", name, socketID);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _manager.RemoveSocket(socketID);
                }
            }
        }
    }
}