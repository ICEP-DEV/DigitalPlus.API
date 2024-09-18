using System.ComponentModel.DataAnnotations;

namespace DigitalPlus.API.Model
{
    public class Department
    {
        [Key]
        public int Department_Id { get; set; }
        public required string Department_Name { get; set; }
    }
}
