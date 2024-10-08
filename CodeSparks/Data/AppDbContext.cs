﻿using CodeSparks.Data.Models;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeSparks.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IDataProtectionKeyContext
    {
        public DbSet<AppMetadata> AppMetadata { get; set; }
        public DbSet<Spark> Sparks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<QuestProgress> QuestProgress { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<SparkComment> SparkComments { get; set; }
        public DbSet<SparkUserStatus> SparkStatuses { get; set; }
        public DbSet<SocialLink> SocialLinks { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }


        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
