
using System;

namespace Municipal_services_app.Models
{
    public class Event
    {
        public int EventID { get; set; } // PK
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime Date { get; set; }
        public string Location { get; set; } = "";
        public string Category { get; set; } = "";
        public string Organizer { get; set; } = "";
        public int Priority { get; set; } = 0; 
    }
}