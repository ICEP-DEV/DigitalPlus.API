using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    // Enhanced Announcement Type Enum
    public enum AnnouncementType
    {
        OneTime,
        Recurring,
        Drip
    }

    // Announcement Frequency for Recurring/Drip Announcements
    public enum AnnouncementFrequency
    {
        TwoWeeks,
        Monthly,
        Quarterly
    }
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        public string AnnouncementTitle { get; set; }   // New required field
        public AnnouncementUserRole UserRole { get; set; }
        public AnnouncementType Type { get; set; }
        public DateTime AnnouncementDate { get; set; }
        public string AnnouncementContent { get; set; }
        public byte[]? AnnouncementImage { get; set; }
        public bool IsImageUpload { get; set; }
        public AnnouncementFrequency? Frequency { get; set; }
        public int? TotalOccurrences { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
