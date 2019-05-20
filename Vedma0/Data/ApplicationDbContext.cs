using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vedma0.Models;
using Vedma0.Models.GameEntities;
using Vedma0.Models.Logging;

namespace Vedma0.Data
{
    public class ApplicationDbContext : IdentityDbContext<VedmaUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Game> Game { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<DiaryPage> Diary { get; set; }
        public DbSet<ErrorItem> Errors { get; set; }
        public DbSet<VerboseItem> Verbose { get; set; }
        public DbSet<DebugItem> Debug { get; set; }
    }
}
