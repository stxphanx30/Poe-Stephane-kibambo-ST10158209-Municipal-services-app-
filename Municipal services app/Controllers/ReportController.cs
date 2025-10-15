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
    public class ReportController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ReportController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        // GET: /Report/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new[]
            {
                "Sanitation","Roads","Utilities","Water","Electricity","Parks"
            }.Select(c => new SelectListItem { Text = c, Value = c });

            return View(new IssueCreateVm());
        }

        // POST: /Report/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IssueCreateVm vm)
        {
            ViewBag.Categories = new[]
            {
                "Sanitation","Roads","Utilities","Water","Electricity","Parks"
            }.Select(c => new SelectListItem { Text = c, Value = c });

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please correct the highlighted fields.";
                return View(vm);
            }

            // ---- Upload ----
            string? relPath = null;
            const long MaxBytes = 8L * 1024 * 1024; // 8 MB
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf" };

            if (vm.Attachment is not null && vm.Attachment.Length > 0)
            {
                if (vm.Attachment.Length > MaxBytes)
                {
                    ModelState.AddModelError(nameof(vm.Attachment), "File too large (max 8 MB).");
                    return View(vm);
                }

                var ext = Path.GetExtension(vm.Attachment.FileName).ToLowerInvariant();
                if (!allowed.Contains(ext))
                {
                    ModelState.AddModelError(nameof(vm.Attachment), "Only images (jpg, png, gif) or PDF are allowed.");
                    return View(vm);
                }

                //  (App_Data/Uploads)
                var uploadsDir = Path.Combine(_env.ContentRootPath, "App_Data", "Uploads");
                Directory.CreateDirectory(uploadsDir);

                var baseName = Path.GetFileNameWithoutExtension(vm.Attachment.FileName);
                if (string.IsNullOrWhiteSpace(baseName)) baseName = "attachment";
                var fileName = $"{baseName}_{Guid.NewGuid():N}{ext}";
                var fullPath = Path.Combine(uploadsDir, fileName);

                using (var stream = System.IO.File.Create(fullPath))
                {
                    await vm.Attachment.CopyToAsync(stream);
                }


                relPath = Path.Combine("App_Data", "Uploads", fileName).Replace('\\', '/');
            }


            var entity = new Issue
            {
                Location = vm.Location,
                Category = vm.Category,
                Description = vm.Description,
                AttachmentPath = relPath,
                CreatedAt = DateTime.UtcNow,
                Status = "Submitted"
            };

            _db.Issues.Add(entity);
            await _db.SaveChangesAsync();
            TempData["Success"] = "submitted.";

            return RedirectToAction(nameof(Success), new { id = entity.Id });
        }

        // GET: /Report/Success/{id}
        [HttpGet]
        public async Task<IActionResult> Success(int id)
        {
            var issue = await _db.Issues.FirstOrDefaultAsync(i => i.Id == id);
            if (issue is null)
            {
                TempData["Error"] = "Issue not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }


    }
}

