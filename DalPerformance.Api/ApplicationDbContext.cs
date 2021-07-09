using System;
using Microsoft.EntityFrameworkCore;

namespace DalPerformance.Api
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity>()
                .HasKey(e => e.Id);
        }

        public void SeedData()
        {
            Entities.AddRange(
                new Entity { Id = Guid.NewGuid(), Name = "First" },
                new Entity { Id = Guid.NewGuid(), Name = "Second" });

            SaveChanges();
        }
    }
}
