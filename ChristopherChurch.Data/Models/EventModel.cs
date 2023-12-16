using System;
using System.ComponentModel;

namespace ChristopherChurch.Data.Models
{
    public class EventModel
    {
        public required string EventName { get; set; }
        public required  string Description { get; set; }
        public DateTime EventDate { get; set; }
    }
}

