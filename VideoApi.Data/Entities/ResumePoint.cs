﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VideoApi.Data.Entities
{
    public class ResumePoint
    {
		public string AccountId { get; set; }
        public string VideoId { get; set; }
        public double TimePoint { get; set; }
    }
}
