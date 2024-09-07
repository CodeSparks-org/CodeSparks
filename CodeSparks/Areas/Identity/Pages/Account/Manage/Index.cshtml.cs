﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using CodeSparks.Data;
using CodeSparks.Data.Models;
using CodeSparks.Migrations;
using CodeSparks.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CodeSparks.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public UserManager<AppUser> _userManager;
        public SignInManager<AppUser> _signInManager;
        public ISocialNetworkService _socialNetworkService;

        public IndexModel(
            AppDbContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ISocialNetworkService socialNetworkService)
        {
            _socialNetworkService = socialNetworkService;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public AppUser CurrentUser { get; set; }
            public IList<SocialLink> UserSocialLinks { get; set; }
        }

        private async Task LoadAsync(AppUser user)
        {
            Input = new InputModel
            {
                CurrentUser = await _context.Users.Include(u => u.SocialLinks)
                .SingleOrDefaultAsync(u => u.Id == user.Id),
            };

            var socialLinks = _socialNetworkService.GetSocialNetworkList();
            Input.UserSocialLinks = [];

            Input.CurrentUser = await _userManager.Users
              .Where(u => u.Id == user.Id)
              .Select(u => new AppUser
              {
                  Id = user.Id,
                  UserName = user.UserName,
                  Name = user.Name,
                  Description = u.Description,
                  SocialLinks = u.SocialLinks.ToList()
              })
              .SingleOrDefaultAsync();

            foreach (var link in socialLinks)
            {
                var userLink = Input.CurrentUser.SocialLinks.FirstOrDefault(l => l.Name == link.Name);
                var l = new SocialLink
                {
                    Id = userLink != null ? userLink.Id : Guid.NewGuid(),
                    UserId = userLink != null ? userLink.UserId : user.Id,
                    Name = userLink != null ? link.Name : link.Name,
                    Url = userLink != null ? userLink.Url : ""
                };

                Input.UserSocialLinks.Add(l);
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            user.UserName = user.UserName;
            user.Name = Input.CurrentUser.Name;
            user.Description = Input.CurrentUser.Description;

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (ModelState.IsValid)
            {
                var userLinks = await _context.SocialLinks
                  .Where(l => l.UserId == user.Id)
                  .ToListAsync();

                foreach (var link in Input.UserSocialLinks)
                {
                    var userLink = userLinks.FirstOrDefault(l => l.Id == link.Id);

                    if (link.Url == null)
                    {
                        _context.SocialLinks.Remove(userLink);
                    }
                    else
                    {
                        if (userLink != null)
                        {
                            userLink.Url = link.Url;
                            _context.SocialLinks.Update(userLink);
                        }
                        else
                        {
                            link.UserId = user.Id;
                            _context.SocialLinks.Add(link);
                        }
                    }
                }

                _context.Update(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
