using CodeSparks.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeSparks.Controllers
{
    public class PeopleController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public PeopleController(AppDbContext context, ILogger<HomeController> logger)
        {
            _db = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _db.Users.ToListAsync();

            return View(model);
        }
    }
}
