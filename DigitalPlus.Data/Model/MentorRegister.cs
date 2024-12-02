using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Model
{
    public class MentorRegister
    {
        public int MentorRegisterID { get; set; }

        [ForeignKey("Mentor")]
        public int MentorId { get; set; }
        public string MentorName { get; set; } = string.Empty;

        [ForeignKey("Module")]
        public int ModuleId { get; set; }
        public string ModuleCode { get; set; }=string.Empty;
        public bool IsRegisteractivated { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
