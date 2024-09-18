using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.API.Model
{
    public class AssignKey
    {
        [Key]
        public int KeyId { get; set; }
        public Key Key { get; set; }

        [ForeignKey("Mentor")]
        public int MentorId { get; set; }
    }
}
