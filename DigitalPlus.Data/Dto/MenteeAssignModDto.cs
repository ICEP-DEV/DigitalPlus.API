using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalPlus.API.Model;

namespace DigitalPlus.Data.Dto
{
    public class MenteeAssignModDto
    {
      
        public int AssignModId { get; set; }

       
        public int MenteeId { get; set; }

       
        public int ModuleId { get; set; }

       
    }
}
