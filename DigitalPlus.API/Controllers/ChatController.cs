using DigitalPlus.Data.Model;
using DigitalPlus.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        // GET: api/chat/module/{moduleId}
        [HttpGet("module/{moduleId}")]
        public async Task<IActionResult> GetMessagesByModule(int moduleId)
        {
            try
            {
                var messages = await _chatService.GetMessagesByModule(moduleId);
                return Ok(messages);
            }
            catch
            {
                return StatusCode(500, "An error occurred while fetching messages.");
            }
        }

        // POST: api/chat
        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] ChatMessage chatMessage)
        {
            if (chatMessage == null)
                return BadRequest("ChatMessage cannot be null.");

            try
            {
                await _chatService.SaveMessageAsync(chatMessage);
                return Ok("Message saved successfully.");
            }
            catch
            {
                return StatusCode(500, "An error occurred while saving the message.");
            }
        }
    }
}
