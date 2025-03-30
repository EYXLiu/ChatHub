using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading.Tasks;
using ChatHub.Services;
using ChatHub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChatHub.Controllers
{
    [ApiController]
    [Route("/api/history")]
    public class HistoryController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetChatHistory([FromServices] ChatHistory chatHistory)
        {
            var messages = await chatHistory.ChatMessages.OrderBy(m => m.Timestamp).ToListAsync();
            return Ok(messages);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetUserHistory(string name, [FromServices] ChatHistory chatHistory)
        {
            var messages = await chatHistory.ChatMessages.Where(m => m.Sender == name).OrderBy(m => m.Timestamp).ToListAsync();
            return Ok(messages);
        }
    }
}