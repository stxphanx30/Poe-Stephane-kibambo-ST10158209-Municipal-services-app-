using System;
using System.Collections.Generic;

namespace Municipal_services_app.Models
{
    public class EventsIndexViewModel
    {
        public string? Query { get; set; }
        public string? SelectedCategory { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public List<Event> Events { get; set; } = new();
        public List<Event> Recommendations { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public List<Announcement> Announcements { get; set; } = new();
    }
}
