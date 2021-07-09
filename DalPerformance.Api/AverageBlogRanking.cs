using System.Linq;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;

namespace DalPerformance.Api
{
    public class AverageBlogRanking
    {
        [Params(1000)]
        public int NumBlogs;

        [GlobalSetup]
        public void Setup()
        {
            using var context = new BlogDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.SeedData(NumBlogs);
        }

        [Benchmark]
        public double LoadEntities()
        {
            var sum = 0;
            var count = 0;
            using var ctx = new BlogDbContext();
            foreach (var blog in ctx.Blogs)
            {
                sum += blog.Rating;
                count++;
            }

            return (double)sum / count;
        }

        [Benchmark]
        public double LoadEntitiesNoTracking()
        {
            var sum = 0;
            var count = 0;
            using var ctx = new BlogDbContext();
            foreach (var blog in ctx.Blogs.AsNoTracking())
            {
                sum += blog.Rating;
                count++;
            }

            return (double)sum / count;
        }

        [Benchmark]
        public double ProjectOnlyRanking()
        {
            var sum = 0;
            var count = 0;
            using var ctx = new BlogDbContext();
            foreach (var rating in ctx.Blogs.Select(b => b.Rating))
            {
                sum += rating;
                count++;
            }

            return (double)sum / count;
        }

        [Benchmark(Baseline = true)]
        public double CalculateInDatabase()
        {
            using var ctx = new BlogDbContext();
            return ctx.Blogs.Average(b => b.Rating);
        }
    }
}
