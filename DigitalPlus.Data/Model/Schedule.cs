﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPlus.API.Model
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [ForeignKey("Mentor")]
        public int MentorId { get; set; }

        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public string TimeSlot { get; set; }
        public string DaysOfTheWeek { get; set; }
    }
}
