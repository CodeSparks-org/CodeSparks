using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeSparks.Data;
using CodeSparks.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace CodeSparks.Controllers
{
    public class SparksController : Controller
    {
        private readonly AppDbContext _context;

        public SparksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Sparks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sparks.ToListAsync());
        }

        // GET: Sparks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spark = await _context.Sparks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spark == null)
            {
                return NotFound();
            }

            return View(spark);
        }

        // GET: Sparks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sparks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Status,CreatedDate")] Spark spark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spark);
        }

        // GET: Sparks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spark = await _context.Sparks.FindAsync(id);
            if (spark == null)
            {
                return NotFound();
            }
            return View(spark);
        }

        // POST: Sparks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Status,CreatedDate")] Spark spark)
        {
            if (id != spark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Sparks.Any(e => e.Id == spark.Id))
                    {
                        throw;
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(spark);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spark = await _context.Sparks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spark == null)
            {
                return NotFound();
            }

            return View(spark);
        }

        // POST: Sparks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spark = await _context.Sparks.FindAsync(id);
            if (spark != null)
            {
                _context.Sparks.Remove(spark);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
