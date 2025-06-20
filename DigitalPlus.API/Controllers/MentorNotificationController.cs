using DigitalPlus.Data.Dto;
using DigitalPlus.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MentorNotificationController : ControllerBase
    {
        private readonly DigitalPlusDbContext _context;

        public MentorNotificationController(DigitalPlusDbContext context)
        {
            _context = context;
        }

        [HttpGet("chat")]
        public async Task<IActionResult> GetChatNotifications()
        {
            var chatNotifications = await _context.ChatMessages
                .OrderByDescending(m => m.Timestamp)
                .Take(10)
                .Select(m => new MentorNotificationDto
                {
                    Sender = m.Sender,
                    Message = $"sent a message: {m.Message}",
                    Timestamp = m.Timestamp.ToString("g"),
                    ProfileImg = ""
                }).ToListAsync();

            return Ok(chatNotifications);
        }


        [HttpGet("booking")]
        public async Task<IActionResult> GetBookingNotifications()
        {
            var bookingNotifications = await _context.Bookings
                .OrderByDescending(b => b.BookingDateTime)
                .Take(10)
                .Select(b => new MentorNotificationDto
                {
                    Sender = $"Booking by Mentee {b.MenteeId}",
                    Message = $"Session with Mentor {b.MentorId} - {b.SessionType}",
                    Timestamp = b.BookingDateTime.ToString("g"),
                    ProfileImg = ""
                }).ToListAsync();

            return Ok(bookingNotifications);
        }

        [HttpGet("announcement")]
        public async Task<IActionResult> GetAnnouncementNotifications()
        {
            var announcementNotifications = await _context.Announcements
                .OrderByDescending(a => a.AnnouncementDate)
                .Take(10)
                .Select(a => new MentorNotificationDto
                {
                    Sender = "Announcement",
                    Message = $"{a.AnnouncementTitle}",
                    Timestamp = a.AnnouncementDate.ToString("g"),
                    ProfileImg = ""
                }).ToListAsync();

            return Ok(announcementNotifications);
        }
    }
}

