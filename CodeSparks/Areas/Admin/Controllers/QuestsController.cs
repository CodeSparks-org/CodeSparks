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
    public class QuestsController : Controller
    {
        private readonly AppDbContext _context;

        public QuestsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Quests
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Quests.Include(q => q.Owner);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Quests/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests
                .Include(q => q.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quest == null)
            {
                return NotFound();
            }

            return View(quest);
        }

        // GET: Admin/Quests/Create
        public IActionResult Create()
        {
            ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/Quests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,XPReward,OwnerUserId")] Quest quest)
        {
            if (ModelState.IsValid)
            {
                quest.Id = Guid.NewGuid();
                _context.Add(quest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", quest.OwnerUserId);
            return View(quest);
        }

        // GET: Admin/Quests/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests.FindAsync(id);
            if (quest == null)
            {
                return NotFound();
            }
            ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", quest.OwnerUserId);
            return View(quest);
        }

        // POST: Admin/Quests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,XPReward,OwnerUserId")] Quest quest)
        {
            if (id != quest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestExists(quest.Id))
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
            ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", quest.OwnerUserId);
            return View(quest);
        }

        // GET: Admin/Quests/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests
                .Include(q => q.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quest == null)
            {
                return NotFound();
            }

            return View(quest);
        }

        // POST: Admin/Quests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var quest = await _context.Quests.FindAsync(id);
            if (quest != null)
            {
                _context.Quests.Remove(quest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestExists(Guid id)
        {
            return _context.Quests.Any(e => e.Id == id);
        }
    }
}
