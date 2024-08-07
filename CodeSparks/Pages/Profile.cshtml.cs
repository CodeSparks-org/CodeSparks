using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using CodeSparks.Data.Models;
using CodeSparks.Services.Repositories;

namespace CodeSparks.Pages
{
    public class ProfileModel : PageModel
    {
        public AppUser AppUser { get; set; } = new AppUser();
        public string DisplayName => string.IsNullOrEmpty(AppUser.Name) ? AppUser.UserName ?? string.Empty : AppUser.Name;

        public int XPForNextLevel { get; set; }
        public double XPProgressPercentage { get; set; }
        public List<Badge> Badges { get; set; } = new List<Badge>();

        private readonly IUserRepository _userRepository;
        private readonly IBadgeRepository _badgeRepository;

        public ProfileModel(IUserRepository userRepository, IBadgeRepository badgeRepository)
        {
            _userRepository = userRepository;
            _badgeRepository = badgeRepository;
        }

        public void OnGet(string userName)
        {
            AppUser = _userRepository.GetByUserName(userName);
            if (AppUser != null)
            {
                XPForNextLevel = GetXPForNextLevel(AppUser.Level);
                XPProgressPercentage = (AppUser.XP / (double)XPForNextLevel) * 100;
                Badges = _badgeRepository.GetBadgesByUserId(AppUser.Id);
            }
            else
            {
                // Handle case where user is not found
                RedirectToPage("/Error");
            }
        }

        private int GetXPForNextLevel(int level)
        {
            return 100 * level * level;
        }
    }
}
