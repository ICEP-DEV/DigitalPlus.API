using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.API.Model
{
    public class Administrator
    {
        [Key]
        public int Admin_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string ContactNo { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public string Password { get; set; }
        
        [NotMapped]
        public byte[] ImageData { get; set; }

        // This is used for file uploads, but should not be mapped to the database
        [NotMapped]
        public IFormFile Image { get; set; }

    }
}
