using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.API.Model
{
    public class MentorReport
    {
        [Key]
        public int ReportId { get; set; }

        [ForeignKey("Mentor")]
        public int MentorId { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow; // Automatically sets current timestamp

        [Required]
        public string Month { get; set; }

        public int NoOfStudents { get; set; }
        public string Remarks { get; set; }
        public string Challenges { get; set; }
        public string SocialEngagement { get; set; }
    }
}