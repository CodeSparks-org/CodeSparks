using CodeSparks.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeSparks.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<long>, long>
    {
        public DbSet<AppMetadata> AppMetadata { get; set; }
        public DbSet<Spark> Sparks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
