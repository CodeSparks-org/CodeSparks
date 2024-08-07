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
    public class UserSkillsController : Controller
    {
        private readonly AppDbContext _context;

        public UserSkillsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UserSkills
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserSkills.Include(u => u.Skill).Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/UserSkills/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSkill = await _context.UserSkills
                .Include(u => u.Skill)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSkill == null)
            {
                return NotFound();
            }

            return View(userSkill);
        }

        // GET: Admin/UserSkills/Create
        public IActionResult Create()
        {
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/UserSkills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SkillId,UserId,SkillLevel")] UserSkill userSkill)
        {
            if (ModelState.IsValid)
            {
                userSkill.Id = Guid.NewGuid();
                _context.Add(userSkill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id", userSkill.SkillId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userSkill.UserId);
            return View(userSkill);
        }

        // GET: Admin/UserSkills/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSkill = await _context.UserSkills.FindAsync(id);
            if (userSkill == null)
            {
                return NotFound();
            }
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id", userSkill.SkillId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userSkill.UserId);
            return View(userSkill);
        }

        // POST: Admin/UserSkills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,SkillId,UserId,SkillLevel")] UserSkill userSkill)
        {
            if (id != userSkill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSkill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSkillExists(userSkill.Id))
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
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id", userSkill.SkillId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userSkill.UserId);
            return View(userSkill);
        }

        // GET: Admin/UserSkills/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSkill = await _context.UserSkills
                .Include(u => u.Skill)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSkill == null)
            {
                return NotFound();
            }

            return View(userSkill);
        }

        // POST: Admin/UserSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userSkill = await _context.UserSkills.FindAsync(id);
            if (userSkill != null)
            {
                _context.UserSkills.Remove(userSkill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSkillExists(Guid id)
        {
            return _context.UserSkills.Any(e => e.Id == id);
        }
    }
}
