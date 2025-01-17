using DigitalPlus.Data.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Dto
{
    public class AnnouncementCreateDto
    {

        public string AnnouncementTitle { get; set; }
        public AnnouncementUserRole UserRole { get; set; }
        public DateTime AnnouncementDate { get; set; }
        public string AnnouncementContent { get; set; }
        public byte[] AnnouncementImage { get; set; }
        public DateTime? EndDate { get; set; }

        public IFormFile AnnouncementImageFile { get; set; }
    }
}
