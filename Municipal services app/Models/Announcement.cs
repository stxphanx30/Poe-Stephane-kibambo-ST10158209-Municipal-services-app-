
   using System;

   namespace Municipal_services_app.Models
{
        public class Announcement
        {
            public int AnnouncementID { get; set; } // PK
            public string Title { get; set; } = "";
            public string Message { get; set; } = "";
            public DateTime DatePosted { get; set; }
            public string Category { get; set; } = "";
        }
    }