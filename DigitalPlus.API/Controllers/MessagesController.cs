using DigitalPlus.Data.Dto;
using DigitalPlus.Data.Model;
using DigitalPlus.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly DigitalPlusDbContext _context;

        public MessagesController(DigitalPlusDbContext context)
        {
            _context = context;
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage([FromForm] SendMessageDto messageDto)
        {
            try
            {
                // Create the new Message entity
                var message = new Message
                {
                    SenderId = messageDto.SenderId,
                    ReceiverId = messageDto.ReceiverId,
                    MessageText = messageDto.MessageText,
                    ModuleId = messageDto.ModuleId,
                    Timestamp = DateTime.UtcNow
                };

                // If there's a file, store it:
                if (messageDto.File != null)
                {
                    // Convert the file to byte[]
                    using var memoryStream = new MemoryStream();
                    await messageDto.File.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    var attachment = new MessageAttachment
                    {
                        FileName = messageDto.File.FileName,
                        FileType = messageDto.File.ContentType,
                        FileContent = fileBytes,
                        // We'll associate it with the message
                        Message = message
                    };

                    // For convenience, store short info in the Message table
                    message.FilePath = attachment.FileName;
                    message.FileType = attachment.FileType;

                    // Save the attachment
                    _context.MessageAttachments.Add(attachment);
                }

                // Save the message
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                // Return the message (or an object) as JSON
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error sending message: {ex.Message}");
            }
        }

        [HttpGet("get-messages/{senderId}/{receiverId}")]
        public async Task<IActionResult> GetMessages(int senderId, int receiverId)
        {
            var messages = await _context.Messages
                .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                            (m.SenderId == receiverId && m.ReceiverId == senderId))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            return Ok(messages);
        }
    }

}
