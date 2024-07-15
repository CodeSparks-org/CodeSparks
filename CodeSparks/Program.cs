using CodeSparks.Data;
using CodeSparks.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var hostingProvider = Environment.GetEnvironmentVariable("HOSTING_PROVIDER");

bool isRender = hostingProvider?.ToLower() == "render.com";
#if DEBUG
isRender = true;
#endif

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<AppUser, IdentityRole<long>>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole<long>>()
    .AddUserManager<UserManager<AppUser>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHealthChecks();

var app = builder.Build();
app.Logger.LogInformation("Application starting up");
if (isRender)
{
    app.Logger.LogInformation("Deployed on render.com");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    if (!isRender)
    {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
}

if (!isRender) {
    app.UseHttpsRedirection();
}
    
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapHealthChecks("/health");

app.Run();
