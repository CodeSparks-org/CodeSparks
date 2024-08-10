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
                Rating = 4.7,
                Progress = 0
            }).ToList();

            return View(skills);
        }

        public IActionResult Details(Guid id)
        {
            var skill = _context.Skills
                .Where(s => s.Id == id)
                .Select(s => new SkillViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    //Level = s.Level,
                    //Progress = 0, //TODO:
                    //RelatedQuests = _context.Quests
                    //    .Where(q => q.SkillId == s.Id)
                    //    .Select(q => new QuestViewModel
                    //    {
                    //        Id = q.Id,
                    //        Title = q.Title,
                    //        XPReward = q.XPReward
                    //    }).ToList(),
                    //SuggestedSkills = _context.Skills
                    //    .Where(sk => sk.Id != s.Id)
                    //    .Select(sk => new SkillViewModel
                    //    {
                    //        Id = sk.Id,
                    //        Name = sk.Name
                    //    }).ToList()
                }).FirstOrDefault();

            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }
    }
}
