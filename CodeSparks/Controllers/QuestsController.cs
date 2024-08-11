using CodeSparks.Data;
using CodeSparks.Data.Models;
using CodeSparks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                    Progress = 0,
                    Rating = 4.7,
                    Status = QuestStatus.Registered //TODO: Placeholder, should map to actual status
                }).ToList();

            return View(quests);
        }

        public IActionResult Start(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
            {
                return NotFound();
            }

            var questProgress = _context.QuestProgresses
                .FirstOrDefault(qp => qp.QuestId == id && qp.UserId == Guid.Parse(userId));

            if (questProgress == null)
            {
                return NotFound();
            }

            // Update the quest progress status
            questProgress.Status = QuestStatus.InProgress;
            questProgress.StartedAt = DateTime.UtcNow;
            _context.SaveChanges();

            // Optionally, pass a message or data to the view
            ViewBag.Message = "Quest started successfully!";
            return View(questProgress); // Render the Start view with the updated progress
        }
    }
}
