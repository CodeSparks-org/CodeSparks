using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeSparks.Data;
using CodeSparks.Data.Models;
using System.Collections;

namespace CodeSparks.Controllers
{
    public class SparksController : Controller
    {
        private readonly AppDbContext _context;

        public SparksController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(SparkCategory? category = null)
        {
            IQueryable<Spark> sparks = _context.Sparks.Where(s => s.IsPublic);
            if (category != null)
            {
                sparks = sparks.Where(s => s.Category == category);
            }

            var model = await sparks.ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Beginner()
        {
            IQueryable<Spark> sparks = _context.Sparks
                .Where(s => s.IsPublic && s.Category == SparkCategory.Beginner);

            var model = await sparks.ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> List(SparkCategory? category = null)
        {
            IQueryable<Spark> sparks = _context.Sparks.Where(s => s.IsPublic);
            if (category != null)
            {
                sparks = sparks.Where(s => s.Category == category);
            }

            var model = await sparks.ToListAsync();
            return View(model);
        }

        // GET: Sparks/Details/5
        public async Task<IActionResult> Details(long? id)
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
        public IActionResult Create(SparkCategory? category = null)
        {
            var spark = new Spark { IsPublic = true, Name = "", Description = "" };
            if (category.HasValue)
            {
                spark.Category = category.Value;
            }
            return View(spark);
        }

        // POST: Sparks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Spark spark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(spark);
        }

        // GET: Sparks/Edit/5
        public async Task<IActionResult> Edit(long? id)
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
        public async Task<IActionResult> Edit(long id, Spark spark)
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
                    if (!SparkExists(spark.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View(spark);
        }

        // GET: Sparks/Delete/5
        public async Task<IActionResult> Delete(long? id)
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
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var spark = await _context.Sparks.FindAsync(id);
            if (spark != null)
            {
                _context.Sparks.Remove(spark);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool SparkExists(long id)
        {
            return _context.Sparks.Any(e => e.Id == id);
        }
    }
}
