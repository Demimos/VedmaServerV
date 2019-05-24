using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vedma0.Models;
using Vedma0.Models.GameEntities;
using Vedma0.Models.Logging;
using Vedma0.Models.ManyToMany;
using Vedma0.Models.Properties;

namespace Vedma0.Data
{
    public class ApplicationDbContext : IdentityDbContext<VedmaUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           // Database.EnsureCreated();
        }

        //public ApplicationDbContext()
        //{
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EntityPreset>()
                .HasKey(t => new { t.GameEntityId, t.PresetId });

            modelBuilder.Entity<EntityPreset>()
                .HasOne(sc => sc.GameEntity)
                .WithMany(s => s.EntityPresets)
                .HasForeignKey(sc => sc.GameEntityId);

            modelBuilder.Entity<EntityPreset>()
                .HasOne(sc => sc.Preset)
                .WithMany(c => c.EntityPresets)
                .HasForeignKey(sc => sc.PresetId);

            modelBuilder.Entity<GameUser>()
           .HasKey(t => new { t.GameId, t.VedmaUserId });

            modelBuilder.Entity<GameUser>()
                .HasOne(sc => sc.Game)
                .WithMany(s => s.GameUsers)
                .HasForeignKey(sc => sc.GameId);

            modelBuilder.Entity<GameUser>()
                .HasOne(sc => sc.VedmaUser)
                .WithMany(c => c.GameUsers)
                .HasForeignKey(sc => sc.VedmaUserId);

            modelBuilder.Entity<Log>()
                .HasOne(sc => sc.Game)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GameEntity>()
              .HasOne(sc => sc.Game)
              .WithMany(c=>c.GameEntities)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Preset>()
             .HasOne(sc => sc.Game)
             .WithMany(c => c.Presets)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BaseProperty>()
             .HasOne(sc => sc.Game)
             .WithMany(c => c.BaseProperties)
             .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DiaryPage>()
               .HasOne(sc => sc.Character)
               .WithMany(c => c.Diary)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TextProperty>();
            modelBuilder.Entity<NumericProperty>();
            modelBuilder.Entity<TextProperty>();
            modelBuilder.Entity<BaseTextProperty>();
            modelBuilder.Entity<BaseNumericProperty>();
            modelBuilder.Entity<BaseTextArrayProperty>();
           
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<DiaryPage> Diary { get; set; }
        public DbSet<ErrorItem> Errors { get; set; }
        public DbSet<VerboseItem> Verbose { get; set; }
        public DbSet<DebugItem> Debug { get; set; }
        public DbSet<GameEntity> GameEntities { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Preset> Presets { get; set; }
        public DbSet<BaseProperty> BaseProperties { get; set; }
        public DbSet<EntityProperty> Properties { get; set; }
        public DbSet<GameUser> GameUsers { get; set; }
    }
}
