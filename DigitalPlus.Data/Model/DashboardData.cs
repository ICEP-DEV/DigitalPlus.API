﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Model
{
    public class DashboardData
    {
        public int TotalMentees { get; set; }
        public int ActivatedMentees { get; set; }
        public int DeactivatedMentees { get; set; }
        public int TotalMentors { get; set; }
        public int ActivatedMentors { get; set; }
        public int DeactivatedMentors { get; set; }
    }

}
