﻿using Microsoft.AspNetCore.Identity;

namespace CodeSparks.Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public long XP { get; set; }
        public int Level { get; set; }
        public string Description {get; set;} = string.Empty;
        public virtual ICollection<SocialLink> SocialLinks {get; set;} = [];
        // public string Image {get; set;}
    }
}
