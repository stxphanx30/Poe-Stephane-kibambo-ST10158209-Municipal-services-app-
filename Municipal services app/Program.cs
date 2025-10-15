using Microsoft.EntityFrameworkCore;
using Municipal_services_app.Models;
using MunicipalMvcApp.Data;
using Municipal_services_app.Services;

var builder = WebApplication.CreateBuilder(args);

// --- MVC controllers + views ---
builder.Services.AddControllersWithViews();

// --- SQLite connection ---
var dataDir = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
Directory.CreateDirectory(dataDir);
var dbPath = Path.Combine(dataDir, "municipal.db");
var connectionString = $"Data Source={dbPath}";

// --- Register EF Core + SQLite ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// --- Register EventStore as scoped for DI ---
builder.Services.AddScoped<EventStore>();

var app = builder.Build();

// --- Ensure DB exists and seed data ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();  // creates tables if not exist
    Seeder.EnsureSeedData(db);

    // Preload EventStore
    var store = scope.ServiceProvider.GetRequiredService<EventStore>();
    // no parameter needed: EventStore uses injected DbContext
}

// --- Middleware / pipeline ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseStatusCodePagesWithReExecute("/Home/Status/{0}");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
