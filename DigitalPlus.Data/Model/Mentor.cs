using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.API.Model
{
    public class Mentor
    {
        [Key]
        public int MentorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentEmail { get; set; }
        public string PersonalEmail { get; set; }
        public string ContactNo { get; set; }
        public string Password { get; set; }

        public int Available { get; set; }  // Changed to Guid to match the primary key in Availability

        public bool Activated { get; set; }
        public ICollection<Schedule>? Schedules { get; set; }
        public ICollection<AssignMod>? AssignMods { get; set; }
    }
}
