using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace DigitalPlus.API.Model
{
    public class Register
    {
        [Key]
        public int Register_Id { get; set; }

        [ForeignKey("Mentee")]
        public int MenteeId { get; set; }

        [ForeignKey("Mentor")]
        public int MentorId { get; set; }

        [ForeignKey("Report")]
        public int ReportId { get; set; }
        public string Signature { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
