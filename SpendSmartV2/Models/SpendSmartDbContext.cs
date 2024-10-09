using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SpendSmartV2.Models
{
    public class SpendSmartDbContext: IdentityDbContext
    {
        private readonly DbContextOptions _options;
        public DbSet<Expenses> Expenses { get; set; }

        public SpendSmartDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Expenses>()
                        .Property(e => e.Value)
                        .HasColumnType("decimal(18, 2)");
        }
    }
}
