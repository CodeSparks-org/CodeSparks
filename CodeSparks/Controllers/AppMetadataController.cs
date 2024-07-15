using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeSparks.Data;
using CodeSparks.Data.Models;

namespace CodeSparks.Controllers
{
    public class AppMetadataController : Controller
    {
        private readonly AppDbContext _context;

        public AppMetadataController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AppMetadata
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppMetadata.ToListAsync());
        }

        // GET: AppMetadata/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appMetadata = await _context.AppMetadata
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appMetadata == null)
            {
                return NotFound();
            }

            return View(appMetadata);
        }

        // GET: AppMetadata/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppMetadata/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Value,Type,Updated")] AppMetadata appMetadata)
        {
            if (ModelState.IsValid)
            {
                appMetadata.Id = Guid.NewGuid();
                appMetadata.Updated = appMetadata.Updated.ToUniversalTime();
                _context.Add(appMetadata);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appMetadata);
        }

        // GET: AppMetadata/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appMetadata = await _context.AppMetadata.FindAsync(id);
            if (appMetadata == null)
            {
                return NotFound();
            }
            return View(appMetadata);
        }

        // POST: AppMetadata/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Value,Type,Updated")] AppMetadata appMetadata)
        {
            if (id != appMetadata.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    appMetadata.Updated = appMetadata.Updated.ToUniversalTime();
                    _context.Update(appMetadata);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppMetadataExists(appMetadata.Id))
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
            return View(appMetadata);
        }

        // GET: AppMetadata/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appMetadata = await _context.AppMetadata
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appMetadata == null)
            {
                return NotFound();
            }

            return View(appMetadata);
        }

        // POST: AppMetadata/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var appMetadata = await _context.AppMetadata.FindAsync(id);
            if (appMetadata != null)
            {
                _context.AppMetadata.Remove(appMetadata);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppMetadataExists(Guid id)
        {
            return _context.AppMetadata.Any(e => e.Id == id);
        }
    }
}
