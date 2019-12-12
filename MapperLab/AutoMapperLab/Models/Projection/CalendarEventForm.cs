using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapperLab.Models.Projection
{
    public class CalendarEventForm
    {
        public DateTime EventDate { get; set; }
        public int EventHour { get; set; }
        public int EventMinute { get; set; }
        public string Title { get; set; }
    }
}
