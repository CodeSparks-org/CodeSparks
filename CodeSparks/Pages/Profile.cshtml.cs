using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using CodeSparks.Data.Models;
using CodeSparks.Services.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CodeSparks.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CodeSparks.Services;

namespace CodeSparks.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly AppDbContext _context;
        public AppUser AppUser { get; set; } = new AppUser();
        public string DisplayName => string.IsNullOrEmpty(AppUser.Name) ? AppUser.UserName ?? string.Empty : AppUser.Name;

        public int XPForNextLevel { get; set; }
        public double XPProgressPercentage { get; set; }
        public List<Badge> Badges { get; set; } = new List<Badge>();

        private readonly IUserRepository _userRepository;
        private readonly IBadgeRepository _badgeRepository;

        public ISocialNetworkService _networkLinksService;
        public IList<SocialLink> UserLinks = [];

        public ProfileModel(
            AppDbContext context,
            IUserRepository userRepository,
            IBadgeRepository badgeRepository,
            ISocialNetworkService service)
        {
            _context = context;
            _userRepository = userRepository;
            _badgeRepository = badgeRepository;
            _networkLinksService = service;
        }

        public async Task<IActionResult> OnGet(string userName)
        {
            AppUser = _userRepository.GetByUserName(userName);

            if (AppUser != null)
            {                
                XPForNextLevel = GetXPForNextLevel(AppUser.Level);
                XPProgressPercentage = (AppUser.XP / (double)XPForNextLevel) * 100;
                Badges = _badgeRepository.GetBadgesByUserId(AppUser.Id);

                await LoadAsync(AppUser);
            }
            
            return Page();
        }
        private async Task LoadAsync(AppUser user)
        {
            var networkLinks = _networkLinksService.GetSocialNetworkList();
            UserLinks = [];

            foreach(var link in networkLinks)
            {
                var l = new SocialLink {
                    Name = link.Name,
                    Url = ""
                };

                UserLinks.Add(l);
            }
            var links = await _context.SocialLinks.Where(l => l.UserId == user.Id).ToListAsync();

            foreach(var link in UserLinks)
            {
                var userLink = links.FirstOrDefault(l => l.Name == link.Name);

                if (userLink != null)
                {
                    link.Url = userLink.Url;
                }
            }
        }
        
        private int GetXPForNextLevel(int level)
        {
            return 100 * level * level;
        }
    }
}
