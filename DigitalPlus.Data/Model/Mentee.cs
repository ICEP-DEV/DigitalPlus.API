using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.API.Model
{
    public class Mentee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int Mentee_Id { get; set; }

        public string FirstName { get; set; }= string.Empty;
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string StudentEmail { get; set; } = string.Empty;
        public string ContactNo { get; set; } = string.Empty;

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public string Password { get; set; } = string.Empty;    
        public string Semester { get; set; } = string.Empty;
        public bool Activated { get; set; } = true;
    }

}
