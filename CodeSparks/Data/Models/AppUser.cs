using Microsoft.AspNetCore.Identity;

namespace CodeSparks.Data.Models
{
    public class AppUser : IdentityUser<long>
    {
        public string? Name { get; set; }
        public long XP { get; set; }
        public int Level { get; set; }
    }
}
