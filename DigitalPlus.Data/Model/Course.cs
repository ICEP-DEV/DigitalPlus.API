using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.API.Model
{
    public class Course
    {
        [Key]
        public int Course_Id { get; set; }
        public required string Course_Name { get; set; }

        public required string Course_Code { get; set; }

        [ForeignKey("Department")]
        public int? Department_Id { get; set; }
    }
}
