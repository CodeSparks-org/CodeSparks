using CodeSparks.Data;
using CodeSparks.Data.Models;
using CodeSparks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeSparks.Controllers
{
    public class QuestsController : Controller
    {
        private readonly AppDbContext _context;

        public QuestsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var quests = _context.Quests
                .Select(q => new QuestViewModel
                {
                    Id = q.Id,
                    Title = q.Title,
                    Description = q.Description,
                    XPReward = q.XPReward,
                    Progress = 10,
                    Rating = 4.3,
                    Status = QuestStatus.Registered //TODO: Placeholder, should map to actual status
                }).ToList();

            return View(quests);
        }
    }
}
