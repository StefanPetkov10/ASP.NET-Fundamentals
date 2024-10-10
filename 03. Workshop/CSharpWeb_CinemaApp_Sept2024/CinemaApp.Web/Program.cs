using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("SQLServer");

// Add services to the container.
builder.Services.AddDbContext<CinemaDbContext>(optins =>
{
    //Like OnConfiguration method

    optins.UseSqlServer(connectionString);
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    {
        //options.SignIn.RequireConfirmedAccount = true;
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<CinemaDbContext>();

builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Authorization can work only if we know who uses the application -> We need Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.ApplyMigrations();
app.Run();
