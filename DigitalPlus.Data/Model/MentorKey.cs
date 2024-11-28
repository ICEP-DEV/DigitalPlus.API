using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Model
{
    public class MentorKey
    {
        [Key]
        public int KeyId { get; set; }

        [ForeignKey("Mentor")]
        public int MentorId { get; set; }

        public string FirstName { get; set; }= string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Contact { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;

    }
}
