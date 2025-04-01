using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading.Tasks;
using ChatHub.Services;
using ChatHub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RESTChatHub.Controllers
{
    [ApiController]
    [Route("/api/userHistory")]
    public class PreviousUserController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserHistory([FromServices] UserHistory userHistory)
        {
            var messages = await userHistory.UserInfo.OrderBy(m => m.name).ToListAsync();
            return Ok(messages);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetUserHistory(string name, [FromServices] UserHistory userHistory)
        {
            var messages = await userHistory.UserInfo.Where(m => m.name == name).ToListAsync();
            return Ok(messages);
        }
    }
}