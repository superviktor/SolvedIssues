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

            //computed column;
            modelBuilder.Entity<Entity>()
                .Property(e => e.Name)
                .HasComputedColumnSql("[Name]+suffix");

            modelBuilder.Entity<SubEntity>()
                .HasKey(se => se.Id);

            modelBuilder.Entity<SubEntity>()
                .HasOne(se => se.Entity)
                .WithMany(e => e.SubEntities)
                .HasForeignKey(se => se.EntityId);
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
