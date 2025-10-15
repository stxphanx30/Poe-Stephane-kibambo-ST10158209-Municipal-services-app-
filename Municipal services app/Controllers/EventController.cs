using Microsoft.AspNetCore.Mvc;

namespace Municipal_services_app.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Load()
        {
            return View();
        }
    }
}
