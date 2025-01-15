using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Model
{
    public class MessageAttachment
    {
        [Key]
        public int AttachmentId { get; set; } // Primary Key
        public int MessageId { get; set; } // Foreign Key to Messages
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public byte[] FileContent { get; set; } = Array.Empty<byte>();

        public Message Message { get; set; } // Navigation property
    }
}
