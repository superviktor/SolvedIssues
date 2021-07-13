using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DalPerformance.Api
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server = localhost; Database=main;User=sa;Password=complexPwd1234;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasKey(e => e.Id);
        }

        public void SeedData(int numblogs)
        {
            Blogs.AddRange(
                Enumerable.Range(0, numblogs).Select(
                    i => new Blog
                    {
                        Name = $"Blog{i}",
                        Url = $"blog{i}.blogs.net",
                        CreationTime = new DateTime(2020, 1, 1),
                        Rating = i % 5
                    }));
            SaveChanges();
        }
    }
}
