using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.API.Model
{
    public class Mentor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // MentorId is not auto-generated
        public int MentorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string StudentEmail { get; set; }  // No [Key] here, we will make it unique

        [Required]
        public string PersonalEmail { get; set; }  // No [Key] here, we will make it unique

        public string ContactNo { get; set; }
        public string Password { get; set; }
        public int Available { get; set; }
        public bool Activated { get; set; }
    }
}
