using Microsoft.AspNetCore.Mvc;
using Municipal_services_app.Services;
using Municipal_services_app.Models;
using System;
using System.Linq;

namespace Municipal_services_app.Controllers
{
  
    public class EventController : Controller
    {
        private readonly EventStore _store;

        public EventController(EventStore store)
        {
            _store = store;
        }

        public IActionResult Load(string? q, string? category, DateTime? from, DateTime? to)
        {
            if (category == "All") category = null;
            var results = _store.Search(q, category, from, to);
            var recs = _store.Recommend(5);

            var model = new EventsIndexViewModel
            {
                Query = q,
                SelectedCategory = category,
                From = from,
                To = to,
                Events = results,
                Recommendations = recs,
                Categories = _store.Categories.ToList()
            };
            return View(model);
        }
    }
}
