using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.API.Model
{
    public class Administrator
    {
        [Key]
        public int Admin_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNo { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public string Password { get; set; }

        // Navigation property for schedules
        public ICollection<Schedule>? Schedules { get; set; }
    }
}
