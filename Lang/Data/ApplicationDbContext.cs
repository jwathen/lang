using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Lang.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.IO;

namespace Lang.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ApplicationUser
            builder.Entity<ApplicationUser>()
                .HasMany(x => x.Languages)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            builder.Entity<ApplicationUser>()
                .HasMany(x => x.ChatParticipation)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            // ChatParticipant
            builder.Entity<ChatParticipant>()
                .HasKey(x => new { x.ChatSessionId, x.UserId });

            // ChatSession
            builder.Entity<ChatSession>()
                .HasMany(x => x.Participants)
                .WithOne(x => x.ChatSession)
                .HasForeignKey(x => x.ChatSessionId);
            builder.Entity<ChatSession>().ToTable("ChatSessions");

            // Language
            builder.Entity<Language>().ToTable("Languages");

            // UserLanguage
            builder.Entity<UserLanguage>()
                .HasOne(x => x.Language)
                .WithMany(x => x.UserLanguages)
                .HasForeignKey(x => x.LanguageId);

            Seed(builder);
        }

        private void Seed(ModelBuilder builder)
        {
            string languagesData = Path.Combine((string)AppDomain.CurrentDomain.GetData("DataDirectory"), "languages.json");
            var languages = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(languagesData));
            builder.Entity<Language>()
                .HasData(languages.Select(x => new Language { Id = x.Key, Name = x.Value }).ToArray());
        }

        public virtual DbSet<ChatParticipant> ChatParticipants { get; set; }
        public virtual DbSet<ChatSession> ChatSessions { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<UserLanguage> UserLanguages { get; set; }
    }
}
