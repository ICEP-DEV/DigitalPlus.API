using DigitalPlus.Data.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Dto
{
    public class AnnouncementCreateViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Title length can't be more than 100 characters.")]
        public string AnnouncementTitle { get; set; }
        [Required]
        public AnnouncementUserRole UserRole { get; set; }
        [Required]
        public DateTime AnnouncementDate { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Content length can't be more than 500 characters.")]
        public string AnnouncementContent { get; set; }
        public IFormFile? AnnouncementImageFile { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
