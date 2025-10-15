
    using MunicipalMvcApp.Data;
    using Municipal_services_app.Models;
    using System;
    using System.Linq;

    namespace Municipal_services_app.Models
{
        public static class Seeder
        {
            public static void EnsureSeedData(AppDbContext db)
            {
                db.Database.EnsureCreated();

                if (!db.Events.Any())
                {
                    db.Events.AddRange(
                        new Event { EventID = 1, Title = "Community Clean-Up Day", Description = "Join us to clean the park and riverside. Gloves and bags provided.", Date = DateTime.Parse("2025-10-20"), Location = "Cape Town – Green Point Park", Category = "Community", Organizer = "City Council" },
                        new Event { EventID = 2, Title = "Coding Bootcamp for Beginners", Description = "A weekend bootcamp introducing Python and web development.", Date = DateTime.Parse("2025-10-25"), Location = "Johannesburg – Sandton Tech Hub", Category = "Education", Organizer = "DevLearn SA" },
                        new Event { EventID = 3, Title = "Farmers Market & Local Crafts", Description = "Fresh produce, handmade crafts, and local music.", Date = DateTime.Parse("2025-10-19"), Location = "Durban – Victoria Market", Category = "Lifestyle", Organizer = "KZN Community" },
                        new Event { EventID = 4, Title = "Youth Worship Night", Description = "A night of praise, prayer, and fellowship for the youth.", Date = DateTime.Parse("2025-10-18"), Location = "Pretoria – Lighthouse Church", Category = "Faith", Organizer = "Youth Ministry" },
                        new Event { EventID = 5, Title = "Career Fair 2025", Description = "Meet employers, discover internships and graduate programs.", Date = DateTime.Parse("2025-10-28"), Location = "Cape Town – CTICC", Category = "Career", Organizer = "SA Recruitment Agency" },
                        new Event { EventID = 6, Title = "Startup Pitch Night", Description = "Watch local startups pitch to investors and mentors.", Date = DateTime.Parse("2025-10-30"), Location = "Johannesburg – Innovation Loft", Category = "Business", Organizer = "Startup Africa" },
                        new Event { EventID = 7, Title = "Heritage Cultural Festival", Description = "Celebrate diverse African cultures with food, music, and dance.", Date = DateTime.Parse("2025-11-02"), Location = "Durban – Moses Mabhida Grounds", Category = "Culture", Organizer = "City of Durban" },
                        new Event { EventID = 8, Title = "Women in Tech Meetup", Description = "Networking session for women in tech and entrepreneurship.", Date = DateTime.Parse("2025-11-05"), Location = "Johannesburg – Braamfontein Hub", Category = "Networking", Organizer = "WomenForward SA" }
                    );
                }

                if (!db.Announcements.Any())
                {
                    db.Announcements.AddRange(
                        new Announcement { AnnouncementID = 1, Title = "Water Service Interruption", Message = "Notice: Scheduled maintenance on 20 Oct will affect water supply in the CBD area.", DatePosted = DateTime.Parse("2025-10-15"), Category = "Public Notice" },
                        new Announcement { AnnouncementID = 2, Title = "Scholarship Applications Open", Message = "Apply now for the 2026 Youth Development Scholarship Program.", DatePosted = DateTime.Parse("2025-10-12"), Category = "Education" },
                        new Announcement { AnnouncementID = 3, Title = "New Bus Route Launch", Message = "City Transit introduces a new route from Cape Town Station to Green Point.", DatePosted = DateTime.Parse("2025-10-17"), Category = "Transport" },
                        new Announcement { AnnouncementID = 4, Title = "Call for Volunteers", Message = "Looking for volunteers to assist during the Cultural Festival.", DatePosted = DateTime.Parse("2025-10-14"), Category = "Community" },
                        new Announcement { AnnouncementID = 5, Title = "Library Extended Hours", Message = "The Central Library will be open until 8 PM starting 21 Oct.", DatePosted = DateTime.Parse("2025-10-16"), Category = "Public Service" },
                        new Announcement { AnnouncementID = 6, Title = "Weather Alert", Message = "Heavy rainfall expected this weekend – please plan accordingly.", DatePosted = DateTime.Parse("2025-10-18"), Category = "Weather" }
                    );
                }

                db.SaveChanges();
            }
        }
    }

