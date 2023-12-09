using System;
using System.ComponentModel;

namespace ChristopherChurch.Data.Models
{
    public class EventModel
    {
        public int EventId { get; set; }
        public  string EventName { get; set; } = "";
        public  string Description { get; set; } = "";
        public DateTime EventDate { get; set; }
       
    }
}

