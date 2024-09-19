using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Model
{
    public class MentorReport
    {
        [Key]
        public int ReportId { get; set; }

        [ForeignKey("Mentor")]
        public int MentorId { get; set; }

        public string Month { get; set; }=string.Empty;
      
    }
}
