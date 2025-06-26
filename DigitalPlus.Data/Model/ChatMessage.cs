using DigitalPlus.API.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.Data.Model
{
    public class ChatMessage
    {
        [Key]
        public int MessageId { get; set; } // Primary Key

        [ForeignKey("Module")]
        public int ModuleId { get; set; } // Foreign Key referencing the Module table

        public required string Sender { get; set; } // Sender's name or identifier

        public required string Role { get; set; } // Sender's role (e.g., mentor, mentee)

        public string? Message { get; set; } // Optional text message content

        public string? FileName { get; set; } // Optional file name

        public string? FileUrl { get; set; } // Optional file URL

        public DateTime Timestamp { get; set; } = DateTime.Now; // Timestamp with default value

        // Navigation property for the related module
        public LearningModule? Module { get; set; }
    }
}
