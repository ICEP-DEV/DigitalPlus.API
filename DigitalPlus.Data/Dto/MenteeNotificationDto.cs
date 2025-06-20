using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Dto
{
    public class MenteeNotificationDto
    {
        public string Sender { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public string Timestamp { get; set; }
        // Use a string for formatting purposes, e.g., "2 hours ago"
        public DateTime CreatedAt { get; set; }
        public string Type { get; set; } // e.g., "chat", "booking", "announcement", "quiz"
        public string ProfileImg { get; set; } // Optional, if you want to show profile images
        public bool IsRead { get; set; }
    }
}
