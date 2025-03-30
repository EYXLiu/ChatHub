using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading.Tasks;
using ChatHub.Services;

namespace ChatHub.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly WebSocketHandler _webSocketHandler;

        public UserController(WebSocketHandler webSocketHandler) => _webSocketHandler = webSocketHandler;

        [HttpGet]
        public async Task<IActionResult> Connect()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await _webSocketHandler.HandleConnectionAsync(webSocket);
                return Ok();
            }
            else 
            {
                return BadRequest("Websocket connection expected");
            }
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> ConnectName(string name)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await _webSocketHandler.HandleConnectionAsync(webSocket, name);
                return Ok();
            }
            else 
            {
                return BadRequest("Websocket connection expected");
            }
        }
    }
}