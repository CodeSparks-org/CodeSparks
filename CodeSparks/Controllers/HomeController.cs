using CodeSparks.Data;
using CodeSparks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CodeSparks.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _db = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var sparks = await _db.Sparks.Where(s => s.IsPublic).ToListAsync();
            
            return View(sparks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Connect()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
