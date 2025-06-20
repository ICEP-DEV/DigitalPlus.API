using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Model
{
    public enum MenteeNotificationType
    {
        ChatMessage,
        Announcement,
        Booking,
        Quiz
    }
    public class MenteeNotification
    {
        [Key]
        public int Id { get; set; }

        public MenteeNotificationType Type { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsRead { get; set; } = false;

        public string ReferenceId { get; set; } // e.g., ChatId, AnnouncementId, BookingId, QuizId
    }
}
