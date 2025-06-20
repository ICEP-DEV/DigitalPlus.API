using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Dto
{
    public class MentorNotificationDto
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public string Timestamp { get; set; }
        public string ProfileImg { get; set; } // Optional, if you want to show profile images
        public bool IsRead { get; set; }


    }
}
