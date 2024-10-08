﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeSparks.Data;
using CodeSparks.Data.Models;
using System.Collections;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using CodeSparks.ViewModels;

namespace CodeSparks.Controllers
{
    public class SparksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SparksController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? hashtagFilter = null, SparkCategory? selectedCategory = null)
        {
            
            IQueryable<Spark> sparks = _context.Sparks
                .Include(s => s.UserStatuses)
                .Include(s => s.Hashtags)
                .Where(s => s.IsPublic);

            if (selectedCategory != null)
            {
                sparks = sparks
                        .Where(s => s.Category == selectedCategory);
            }

            if (hashtagFilter != null)
            {
                sparks = sparks.Where(s => s.Hashtags.Any(h => hashtagFilter == h.Name));
            }

            ViewData["SelectedCategory"] = selectedCategory;
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

        public async Task<IActionResult> Start(Guid? id)
        {
            var userId = await GetUserIdAsync();
            if (userId == null || id == null)
                return NotFound();

            var status = new SparkUserStatus
            {
                SparkId = (Guid)id,
                StartedDate = DateTime.UtcNow,
                UserId = (Guid)userId,
                Status = SparkStatus.InProgress
            };

            await _context.SparkStatuses.AddAsync(status);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: Sparks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spark = await _context.Sparks
                .Include(s => s.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (spark == null)
            {
                return NotFound();
            }

            var userId = await GetUserIdAsync();
            if(userId != null)
            {
                ViewBag.Status = await _context.SparkStatuses
                    .Where(s => s.UserId == userId && s.SparkId == id)
                    .Select(s => s.Status)
                    .FirstOrDefaultAsync();
            }

            return View(spark);
        }

        // GET: Sparks/Create
        public IActionResult Create(SparkCategory? category = null)
        {
            var spark = new SparkInput { IsPublic = true, Name = "", Description = "" };

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
        public async Task<IActionResult> Create(SparkInput sparkInput)
        {
            var spark = new Spark  //TODO: change to automapper in future.
            {
                Id = Guid.NewGuid(),
                Description = sparkInput.Description,
                Name = sparkInput.Name,
                Category = sparkInput.Category,
                IsPublic = sparkInput.IsPublic,
                Url = sparkInput.Url
            };

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(sparkInput.HashtagList))
                {
                    var hashtags = sparkInput.HashtagList.Split(',')
                    .Where(h => !_context.Hashtags.Any(dbH => dbH.Name == h))
                    .Select(h => {
                        var hashtag = new Hashtag {
                            Name = h.Trim(),
                            SparkId = spark.Id,
                        };

                        return hashtag;
                    })
                    .DistinctBy(h => h.Name)
                    .ToList();

                    _context.Hashtags.AddRange(hashtags);
                }

                _context.Add(spark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(sparkInput);
        }

        // GET: Sparks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
        public async Task<IActionResult> Edit(Guid id, Spark spark)
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
                return RedirectToAction(nameof(Index));
            }
            return View(spark);
        }

        // GET: Sparks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var spark = await _context.Sparks.FindAsync(id);
            if (spark != null)
            {
                _context.Sparks.Remove(spark);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SparkExists(Guid id)
        {
            return _context.Sparks.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Guid id, string commentText)
        {
            if (string.IsNullOrWhiteSpace(commentText))
            {
                // Handle empty comment case
                return RedirectToAction(nameof(Details), new { id });
            }

            var comment = new SparkComment
            {
                UserId = await GetUserIdAsync(),
                Text = commentText,
                DatePosted = DateTime.UtcNow,
                SparkId = id
            };

            _context.SparkComments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        private async Task<Guid?> GetUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            return userId;
        }
    }
}
