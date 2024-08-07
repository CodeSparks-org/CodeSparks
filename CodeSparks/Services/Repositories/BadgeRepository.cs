using CodeSparks.Data;
using CodeSparks.Data.Models;

namespace CodeSparks.Services.Repositories
{
    public interface IBadgeRepository
    {
        List<Badge> GetBadgesByUserId(long userId);
        void AddBadge(Badge badge);
    }

    public class BadgeRepository : IBadgeRepository
    {
        private readonly AppDbContext _context;

        public BadgeRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Badge> GetBadgesByUserId(long userId)
        {
            return _context.Badges.Where(b => b.UserId == userId).ToList();
        }

        public void AddBadge(Badge badge)
        {
            _context.Badges.Add(badge);
            _context.SaveChanges();
        }
    }

}
