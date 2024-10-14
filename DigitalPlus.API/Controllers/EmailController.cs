using System.Threading.Tasks;
using DigitalPlus.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPlus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ISendEmail _emailService;

        // Inject the email service through constructor
        public EmailController(ISendEmail emailService)
        {
            _emailService = emailService;
        }

        // POST api/email/send
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
        {
            if (emailRequest == null || string.IsNullOrEmpty(emailRequest.Email) || string.IsNullOrEmpty(emailRequest.Subject) || string.IsNullOrEmpty(emailRequest.Message))
            {
                return BadRequest("Invalid email request");
            }

            try
            {
                await _emailService.SendEmailAsync(emailRequest.Email, emailRequest.Subject, emailRequest.Message);
                return Ok("Email sent successfully");
            }
            catch (System.Exception ex)
            {
                // Log error (not implemented here) and return failure
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    // DTO for sending email requests
    public class EmailRequest
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
