using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vedma0.Models;

namespace Vedma0.Data
{
    public class ApplicationDbContext : IdentityDbContext<VedmaUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Vedma0.Models.Game> Game { get; set; }
     }
}
