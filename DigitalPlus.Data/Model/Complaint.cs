﻿using DigitalPlus.API.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.API.Model
{
    public class Complaint
    {
        [Key]  // Declares ComplaintId as the Primary Key
        public int ComplaintId { get; set; }

        public DateTime DateLogged { get; set; }       // Date and time the complaint was logged

        // Mentee information
        
        public string MenteeEmail { get; set; }=string.Empty;

        // Mentor information
        public string MentorName { get; set; } = string.Empty;
        public string MentorEmail { get; set; } = string.Empty;

        // Foreign key to the Module entity
        [ForeignKey("Module")]
        public string ModuleName { get; set; } = string.Empty;

        // Detailed description of the complaint
        public string ComplaintDescription { get; set; } = string.Empty;

        // Status of the complaint (e.g., "Resolved", "Unresolved")
        public string Status { get; set; } = string.Empty;

        // Action (for UI purposes, e.g., "1=Resolved, 2= Unresolved")
        public int Action { get; set; }
    }

}