using CodeSparks.Controllers;
using CodeSparks.Data;
using CodeSparks.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeSparks.Services.Repositories
{
    public interface IUserRepository
    {
        AppUser GetById(string id);
        AppUser GetByUserName(string userName);
        void Update(AppUser user);
        Task<List<AppUser>> GetAllUsers();
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UserRepository(AppDbContext context, ILogger<HomeController> logger,
                                UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public AppUser GetById(string id)
        {
            return _context.Users.Find(id) ?? new AppUser();
        }

        public AppUser GetByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName) ?? new AppUser();
        }

        public void Update(AppUser user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public async Task<List<AppUser>> GetAllUsers()
        {
            var adminRole = await _roleManager.Roles
                .Where(r => r.Name == "Admin")
                .Select(r => r.Id)
                .SingleOrDefaultAsync();
            var adminUserIds = await _context.UserRoles
                                        .Where(ur => ur.RoleId == adminRole)
                                        .Select(ur => ur.UserId)
                                        .ToListAsync();

            var model = await _context.Users
                             .Where(u => !adminUserIds.Contains(u.Id))
                             .ToListAsync();
            return model;
        }
    }

}
