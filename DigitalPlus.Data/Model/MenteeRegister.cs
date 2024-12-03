using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace DigitalPlus.API.Model
{
    public class MenteeRegister
    {
        [Key]
        public int MenteeRegisterId { get; set; }

        [ForeignKey("MentorRegister")]
        public int MentorRegisterId { get; set; }

        [ForeignKey("Mentee")]
        public int MenteeId { get; set; }

        [ForeignKey("Mentor")]
        public int MentorId { get; set; }

        [ForeignKey("Module")]
        public int ModuleId { get; set; }

        public string Signature { get; set; }=string.Empty;
        public double Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;

    }
}
