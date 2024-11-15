using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalPlus.API.Model;

namespace DigitalPlus.Data.Model
{
    public class MenteeAssignModule
    {
        [Key]
        public int AssignModId { get; set; }

        [ForeignKey("Mentee")]
        public int MenteeId { get; set; }

        [ForeignKey("Module")]
        public int ModuleId { get; set; }

        public Module Module { get; set; }
    }

}
