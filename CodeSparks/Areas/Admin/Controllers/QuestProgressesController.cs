using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeSparks.Data;
using CodeSparks.Data.Models;

namespace CodeSparks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuestProgressesController : Controller
    {
        private readonly AppDbContext _context;

        public QuestProgressesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/QuestProgresses
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.QuestProgress.Include(q => q.Quest).Include(q => q.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/QuestProgresses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questProgress = await _context.QuestProgress
                .Include(q => q.Quest)
                .Include(q => q.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questProgress == null)
            {
                return NotFound();
            }

            return View(questProgress);
        }

        // GET: Admin/QuestProgresses/Create
        public IActionResult Create()
        {
            ViewData["QuestId"] = new SelectList(_context.Quests, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/QuestProgresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuestId,UserId,Status,RegisteredAt,StartedAt,CompletedAt,Deadline")] QuestProgress questProgress)
        {
            if (ModelState.IsValid)
            {
                questProgress.Id = Guid.NewGuid();
                _context.Add(questProgress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestId"] = new SelectList(_context.Quests, "Id", "Id", questProgress.QuestId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", questProgress.UserId);
            return View(questProgress);
        }

        // GET: Admin/QuestProgresses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questProgress = await _context.QuestProgress.FindAsync(id);
            if (questProgress == null)
            {
                return NotFound();
            }
            ViewData["QuestId"] = new SelectList(_context.Quests, "Id", "Id", questProgress.QuestId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", questProgress.UserId);
            return View(questProgress);
        }

        // POST: Admin/QuestProgresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,QuestId,UserId,Status,RegisteredAt,StartedAt,CompletedAt,Deadline")] QuestProgress questProgress)
        {
            if (id != questProgress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questProgress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestProgressExists(questProgress.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestId"] = new SelectList(_context.Quests, "Id", "Id", questProgress.QuestId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", questProgress.UserId);
            return View(questProgress);
        }

        // GET: Admin/QuestProgresses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questProgress = await _context.QuestProgress
                .Include(q => q.Quest)
                .Include(q => q.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questProgress == null)
            {
                return NotFound();
            }

            return View(questProgress);
        }

        // POST: Admin/QuestProgresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var questProgress = await _context.QuestProgress.FindAsync(id);
            if (questProgress != null)
            {
                _context.QuestProgress.Remove(questProgress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestProgressExists(Guid id)
        {
            return _context.QuestProgress.Any(e => e.Id == id);
        }
    }
}
