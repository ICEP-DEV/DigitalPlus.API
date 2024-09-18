using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.API.Model
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        [ForeignKey("Module")]
        public int ModuleId { get; set; }
        public DateTime Time { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Mentee")]
        public int MenteeId { get; set; }
    }
}
