using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.API.Model
{
    public class AssignMod
    {
        [Key]
        public int AssignModId { get; set; }

        [ForeignKey("Mentor")] 
        public int MentorId { get; set; }

        [ForeignKey("Module")]
        public int ModuleId { get; set; }

        public Module Module { get; set; }

        public Mentor Mentor { get; set; }
    }
}
