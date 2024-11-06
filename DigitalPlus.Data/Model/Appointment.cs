using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.API.Model
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [ForeignKey("Mentee")]
        public int StudentNumber { get; set; }

        public string FullNames { get; set; } = string.Empty;

        [ForeignKey("Module")]
        public int ModuleId { get; set; }

        [ForeignKey("Mentor")]
        public int MentorId { get; set; }

        public string LessonType { get; set; } = string.Empty;

        public DateTime DateTime { get; set; }

        public int Action { get; set; }
    }
}
