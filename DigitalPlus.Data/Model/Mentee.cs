using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.API.Model
{
    public class Mentee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int Mentee_Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentEmail { get; set; }
        public string ContactNo { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public string Password { get; set; }
        public string Semester { get; set; }
    }

}
