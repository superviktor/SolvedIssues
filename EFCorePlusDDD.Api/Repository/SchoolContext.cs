using EFCorePlusDDD.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCorePlusDDD.Api.Repository
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student").HasKey(k => k.Id);
                entity.Property(x => x.Id);
                entity.Property(x => x.Email);
                entity.Property(x => x.Name);
                entity.Property(x => x.FavouriteCourseId);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course").HasKey(k => k.Id);
                entity.Property(x => x.Id);
                entity.Property(x => x.Name);
            });
        }
    }
}
