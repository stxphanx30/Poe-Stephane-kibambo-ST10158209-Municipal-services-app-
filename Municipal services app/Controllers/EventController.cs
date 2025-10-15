using Microsoft.AspNetCore.Mvc;
using Municipal_services_app.Models;
using Municipal_services_app.Services;
using MunicipalMvcApp.Data;
using System;
using System.Linq;

namespace Municipal_services_app.Controllers
{
  
   
        public class EventController : Controller
        {
            private readonly EventStore _store;
            private readonly AppDbContext _db;

            public EventController(EventStore store, AppDbContext db)
            {
                _store = store;
                _db = db;
            }

            public IActionResult Load(string? q, string? category, DateTime? from, DateTime? to)
            {
                if (category == "All") category = null;

                var results = _store.Search(q, category, from, to);
                var recs = _store.Recommend(5);

                var announcements = _db.Announcements
                                       .OrderByDescending(a => a.DatePosted)
                                       .ToList();

                var model = new EventsIndexViewModel
                {
                    Query = q,
                    SelectedCategory = category,
                    From = from,
                    To = to,
                    Events = results,
                    Recommendations = recs,
                    Categories = _store.Categories.ToList(),
                    Announcements = announcements
                };

                return View(model);
            }

        }
    }

