using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Model
{

    public enum AnnouncementUserRole
    {
        Mentee,
        Mentor,
        Both
    }

    public class Announcement
    {
        [Key]
        public int AnnouncementId { get; set; }
        public string AnnouncementTitle { get; set; }   = string.Empty;
        public AnnouncementUserRole UserRole { get; set; }
        public DateTime AnnouncementDate { get; set; }
        public string AnnouncementContent { get; set; }=string.Empty;
        public byte[]? AnnouncementImage { get; set; }
        public DateTime? EndDate { get; set; }


        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
