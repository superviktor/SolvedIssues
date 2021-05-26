using EFCorePlusDDD.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCorePlusDDD.Api
{
    public class SchoolContext : DbContext
    {
        private readonly string _connectionString;
        private readonly bool _useLogger;

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public SchoolContext(string connectionString, bool useLogger)
        {
            _connectionString = connectionString;
            _useLogger = useLogger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionString)
                .UseLazyLoadingProxies();

            if (_useLogger)
            {
                var loggerFactory = LoggerFactory.Create(builder =>
                 {
                     builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                     .AddConsole();
                 });

                optionsBuilder.UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student")
                    .HasKey(k => k.Id);
                entity.Property(x => x.Id);
                entity.Property(x => x.Email);
                entity.Property(x => x.Name);
                entity.HasOne(x => x.FavoriteCourse).WithMany();
                entity.HasMany(x => x.Enrollments)
                    .WithOne(e => e.Student)
                    .OnDelete(DeleteBehavior.Cascade)
                    .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course")
                    .HasKey(k => k.Id);
                entity.Property(x => x.Id);
                entity.Property(x => x.Name);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("Enrollment")
                    .HasKey(x => x.Id);
                entity.HasOne(x => x.Student)
                    .WithMany(s => s.Enrollments);
                entity.HasOne(x => x.Course).WithMany();
                entity.Property(x => x.Grade);
            });
        }
    }
}
