// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Xml;
using CodeSparks.Data;
using CodeSparks.Data.Models;
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
        public ISocialNetworkService _networkLinksService;
        public IEnumerable<SocialNetwork> SocialNetworks;

        [BindProperty]
        public AppUser LoggedUser {get; set;}

        [BindProperty]
        public IList<PlatformLink> UserNetworkLinks {get; set;}

        public IndexModel(
            AppDbContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ISocialNetworkService service)
        {
           _networkLinksService = service;
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
        }

        private async Task LoadAsync(AppUser user)
        {
            var networkLinks = _networkLinksService.GetSocialNetworkList();
            var linkList = new List<PlatformLink>();
            UserNetworkLinks = [];

            foreach(var link in networkLinks)
            {
              var l = new PlatformLink {
                Name = link.Name,
                Url = ""
              };

              UserNetworkLinks.Add(l);
            }

            LoggedUser = await _userManager.Users
              .Where(u => u.Id == user.Id)
              .Select(u => new AppUser {
                Id = user.Id,
                UserName = user.UserName,
                Description = u.Description,
                Links = user.Links.ToList()
              })
              .SingleOrDefaultAsync();

              if (LoggedUser.Links.Count != 0)
              {
                //TODO: set user links to networkLinks
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
            user.UserName = LoggedUser.UserName;
            user.Description = LoggedUser.Description;

            if (user == null) {
              return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (ModelState.IsValid)
            {
              var userLinks = await _context.PlatformLinks
                .Where(l => l.UserId == user.Id)
                .ToListAsync();
            
              foreach(var link in UserNetworkLinks)
              {
                if (link.Url == null)
                {
                  var linkToRemove = userLinks.FirstOrDefault(l => l.Id == link.Id);

                  if (linkToRemove != null)
                  {
                    _context.PlatformLinks.Remove(linkToRemove);
                  }
                } else {
                  var existingLink = userLinks.FirstOrDefault(l => l.Id == link.Id);

                  if (existingLink != null)
                  {
                    existingLink = link;
                  }
                  else
                  {
                    link.UserId = user.Id;
                    _context.PlatformLinks.Add(link);
                  }
                }
              }

              // user.Links = UserNetworkLinks.Where(l => l.Url != null).ToList();
              user.Links = UserNetworkLinks;
              _context.Update(user);

              await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
