using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Dto
{
    public class AssignModDto
    {
        public int AssignModId { get; set; }
        public int MentorId { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }  // Assuming Module has a Name field
        public string ModuleCode { get; set; }
        public string ModuleDescription { get; set; }  // Assuming Module has a Description field
    }
}
