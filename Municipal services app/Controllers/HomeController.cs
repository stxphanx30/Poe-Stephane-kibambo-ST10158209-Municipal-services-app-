using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MunicipalMvcApp.ViewModels;
using MunicipalMvcApp.Data;
using MunicipalMvcApp.Models;
using System.IO;

namespace MunicipalMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public HomeController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index() => View();
        public IActionResult About() => View();
        public IActionResult Privacy() => View();

        

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Home/Error")]
        public IActionResult Error()
        {
      
           
            return View(); // Views/Home/Error.cshtml
        }

        [HttpGet("Home/Status/{code:int}")]
        public IActionResult Status(int code)
        {
            ViewBag.StatusCode = code;
            string title = "Unexpected error";
            string message = "Something went wrong.";

            switch (code)
            {
                case 404: title = "Page not found"; message = "The page you requested doesn’t exist."; break;
                case 403: title = "Forbidden"; message = "You don’t have permission to access this resource."; break;
                case 400: title = "Bad request"; message = "The request could not be understood."; break;
                default: title = $"Error {code}"; message = "An error occurred."; break;
            }

            ViewBag.StatusTitle = title;
            ViewBag.StatusMessage = message;
            return View("Status"); // Views/Home/Status.cshtml
        }

    }
}

