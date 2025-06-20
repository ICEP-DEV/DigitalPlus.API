using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Model
{

    public enum NotificationType
    {
        ChatMessage,
        Announcement,
        Booking
    }


    public class MentorNotification
    {
        [Key]
        public int Id { get; set; }

        public NotificationType Type { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsRead { get; set; } = false;

        public string ReferenceId { get; set; } // e.g., ChatId, AnnouncementId, BookingId
    }
}
