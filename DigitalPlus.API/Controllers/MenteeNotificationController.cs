using DigitalPlus.Data.Dto;
using DigitalPlus.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenteeNotificationController : ControllerBase
    {
        private readonly DigitalPlusDbContext _context;

        public MenteeNotificationController(DigitalPlusDbContext context)
        {
            _context = context;
        }

        [HttpGet("chat")]
        public async Task<IActionResult> GetChatNotifications()
        {
            var chatNotifications = await _context.ChatMessages
                .OrderByDescending(m => m.Timestamp)
                .Take(10)
                .Select(m => new MenteeNotificationDto
                {
                    Sender = m.Sender,
                    Message = $"sent a message: {m.Message}",
                    Timestamp = m.Timestamp.ToString("g"),
                    ProfileImg = "",
                    IsRead = false // Assuming chat messages are unread by default
                }).ToListAsync();

            return Ok(chatNotifications);
        }


        [HttpGet("booking")]
        public async Task<IActionResult> GetBookingNotifications()
        {
            var bookingNotifications = await _context.Bookings
                .OrderByDescending(b => b.BookingDateTime)
                .Take(10)
                .Select(b => new MenteeNotificationDto
                {
                    Sender = $"Booking for Mentor {b.MentorId}",
                    Message = $"Session with Mentee {b.MenteeId} - {b.SessionType}",
                    Timestamp = b.BookingDateTime.ToString("g"),
                    ProfileImg = "",
                    IsRead = false // Assuming bookings are unread by default
                }).ToListAsync();

            return Ok(bookingNotifications);
        }

        [HttpGet("announcement")]
        public async Task<IActionResult> GetAnnouncementNotifications()
        {
            var announcementNotifications = await _context.Announcements
                .OrderByDescending(a => a.AnnouncementDate)
                .Take(10)
                .Select(a => new MenteeNotificationDto
                {
                    Sender = "Announcement",
                    Message = $"{a.AnnouncementTitle}",
                    Timestamp = a.AnnouncementDate.ToString("g"),
                    ProfileImg = ""
                }).ToListAsync();

            return Ok(announcementNotifications);
        }

        [HttpGet("quiz")]
        public async Task<IActionResult> GetQuizNotifications()
        {
            var quizNotifications = await _context.Questions
                .OrderByDescending(q => q.StartDate)
                .Take(10)
                .Select(q => new MenteeNotificationDto
                {
                    Sender = "Quiz",
                    Title = q.Title,
                    Message = "New quiz available",
                    Timestamp = q.StartDate.ToString("g"),
                    ProfileImg = "",
                    Type = "quiz",
                    IsRead = false // Assuming quizzes are unread by default
                }).ToListAsync();

            return Ok(quizNotifications);
        }
    }
}
