using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Data;
using ProductCatalogue.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

// Added Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Added Identity and connected it to EF Core
builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Added Auth Services
builder.Services.AddAuthentication();

// MVC + Api + Razor Support
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

// Added Auth middleware
app.UseAuthentication();
app.UseAuthorization();

// Added MVC + Api + Razor pages routes
app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
