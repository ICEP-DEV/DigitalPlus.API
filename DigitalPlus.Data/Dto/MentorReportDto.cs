using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.API.Dto
{
    public class MentorReportDto
    {
        public int MentorId { get; set; }
        public string Month { get; set; }
        public int NoOfStudents { get; set; }
        public string Remarks { get; set; }
        public string Challenges { get; set; }
        public string SocialEngagement { get; set; }
    }
}
