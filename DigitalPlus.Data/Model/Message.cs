using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Model
{
    public class Message
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string? MessageText { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? FilePath { get; set; }
        public string? FileType { get; set; }
        public int? ReplyToMessageId { get; set; }
        public int ModuleId { get; set; }

    }
}
