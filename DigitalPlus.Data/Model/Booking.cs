using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Model
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; } // Primary key

        [ForeignKey("Mentee")]
        public int MenteeId { get; set; }

        [ForeignKey("Mentor")]
        public int MentorId { get; set; }

        [ForeignKey("Module")]
        public int ModuleId { get; set; }

        [Required]
        public DateTime BookingDateTime { get; set; } // Combined Date and Time of the appointment

        [Required]
        public string SessionType { get; set; } // Session type, e.g., "Online" or "In-person"

        [Required]
        public string Status { get; set; } = "Pending"; // Default to 'Pending'
    }
}
