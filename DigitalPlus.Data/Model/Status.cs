﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Model
{
    public class Status
    {
        public int StatusId { get; set; }

        public string Reason { get; set; } = string.Empty;  

        public DateTime RespondedAt { get; set; }
    }
}
