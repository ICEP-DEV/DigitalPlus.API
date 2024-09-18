using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.API.Model
{
    public class Module
    {
        [Key]
        public int Module_Id { get; set; }
        public required string Module_Name { get; set; }

        public required string Module_Code { get; set; }

        [ForeignKey("Course")]
        public int? Course_Id { get; set; }
    }
}
