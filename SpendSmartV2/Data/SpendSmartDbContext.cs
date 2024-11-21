using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpendSmartV2.Models;

namespace SpendSmartV2.Data
{
    public class SpendSmartDbContext: IdentityDbContext
    {  
        public SpendSmartDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Expenses> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Expenses>()
                        .Property(e => e.Value)
                        .HasColumnType("decimal(18, 2)");
        }
    }
}
