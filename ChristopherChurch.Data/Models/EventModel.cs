using System;
using System.ComponentModel;

namespace ChristopherChurch.Data.Models
{
    public class EventModel
    {
        public string? EventId { get; set; }
        public string? Summary { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string? Location { get; set; }
        public string? AddToCalendarLink { get; set; }
    }
}

