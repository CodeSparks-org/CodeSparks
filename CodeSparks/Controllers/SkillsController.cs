using CodeSparks.Data;
using CodeSparks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeSparks.Controllers
{
    public class SkillsController : Controller
    {
        private readonly AppDbContext _context;

        public SkillsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var skills = _context.Skills
            .Select(s => new SkillViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Rating = (new Random()).NextDouble() * 5, // Generate fake ratings between 0 and 5
                Progress = (new Random()).Next(0, 101) // Generate fake progress between 0 and 100
            }).ToList();

            return View(skills);
        }
    }
}
