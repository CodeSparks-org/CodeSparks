using CodeSparks.Data;
using CodeSparks.Data.Models;
using CodeSparks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> StartAsync(Guid id)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userIdStr == null || !Guid.TryParse(userIdStr, out var userId))
            {
                return NotFound();
            }

            var questProgress = await _context.QuestProgress
                .FirstOrDefaultAsync(qp => qp.QuestId == id && qp.UserId == Guid.Parse(userIdStr));

            if (questProgress == null)
            {
                questProgress = new QuestProgress { QuestId = id, UserId = userId };
                _context.QuestProgress.Add(questProgress);
                await _context.SaveChangesAsync();
            }

            // Update the quest progress status
            questProgress.Status = QuestStatus.InProgress;
            questProgress.StartedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // Optionally, pass a message or data to the view
            ViewBag.Message = "Quest started successfully!";
            questProgress = await _context.QuestProgress
                .Include(q => q.Quest)
                .SingleOrDefaultAsync(qp => qp.QuestId == id && qp.UserId == Guid.Parse(userIdStr));
            return View(questProgress); // Render the Start view with the updated progress
        }
    }
}
