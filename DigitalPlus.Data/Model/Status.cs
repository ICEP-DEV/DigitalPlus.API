using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Model
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }

        public int State { get; set; }

        public string Reason { get; set; } = string.Empty;  

        public DateTime RespondedAt { get; set; }
    }
}
