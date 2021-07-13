using System;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DalPerformance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EntityController(ApplicationDbContext context)
        {
            Debug.WriteLine($"PROCESS ID:{Process.GetCurrentProcess().Id}");
            _context = context;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            //1 efficient querying rules
            //1.1 wrong: load full entity
            var fullEntity = _context.Entities.TagWith("GET ALL");
            //1.2 right: use projection
            var withProjection = _context.Entities.Select(e => e.Name);

            //2 limit result set size
            //2.2 wrong: load full data set 
            var fullDataSet = _context.Entities;
            //2.2 right: limited number of results
            var limited = _context.Entities.Take(25);

            //3 avoid cartesian explosion when loading related entities
            //3.1 wrong: use join
            var joined = _context.Entities
                .Include(e => e.SubEntities);
            //3.2 better: split query
            var splitted = _context.Entities
                .Include(e => e.SubEntities)
                .AsSplitQuery();

            //4 prefer eager to lazy loading
            //4.1 lazy
            foreach (var entity in _context.Entities)
            {
                foreach (var subEntity in entity.SubEntities)
                {
                }
            }
            //4.2 eager
            foreach (var entity in _context.Entities.Select(e => new { e.Name, e.SubEntities }))
            {
                foreach (var subEntity in entity.SubEntities)
                {
                }
            }

            //5 buffering and streaming 
            //5.1 buffering = load all to memory
            var buffered = _context.Entities.ToList();
            //5.2 streaming = one entity at a time
            foreach (var entity in _context.Entities)
            {
            }

            //6 ef tracking
            //6.1 query with tracking state
            var tracked = _context.Entities.ToList();
            //6.2 without tracking
            var asNoTracking = _context.Entities.AsNoTracking().ToList();

            //7 raw sql, user defined functions, views
            var rawSql = _context.Entities.FromSqlRaw("SELECT * FROM Entities");

            //8 use async


            //9 query caching and parametrization 
            //9.1 different expression trees and query plans
            var entity1 = _context.Entities.FirstOrDefault(e => e.Name == "name1");
            var entity2 = _context.Entities.FirstOrDefault(e => e.Name == "name2");
            //9.2 one expression tree and one parametrized execution plan
            var name1 = "name1";
            var name2 = "name2";
            entity1 = _context.Entities.FirstOrDefault(e => e.Name == name1);
            entity2 = _context.Entities.FirstOrDefault(e => e.Name == name2);

            return Ok(fullEntity);
        }

        [HttpPut]
        public IActionResult AddSuffixToName(string suffix)
        {
            //batch update
            foreach (var entity in _context.Entities)
            {
                entity.Name += suffix;
            }

            _context.SaveChanges();

            //bulk update
            _context.Database.ExecuteSqlRaw($"UPDATE [Entities] SET [Name] = [Name] +{suffix}");

            return NoContent();
        }

        [HttpGet("id/{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_context.Entities.SingleOrDefault(x => x.Id == id));
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            return Ok(_context.Entities.SingleOrDefault(x => x.Name == name));
        }

        [HttpGet("benchmarks")]
        public IActionResult Benchmarks()
        {
            var config = new ManualConfig()
                .WithOptions(ConfigOptions.DisableOptimizationsValidator)
                .AddValidator(JitOptimizationsValidator.DontFailOnError)
                .AddLogger(ConsoleLogger.Default)
                .AddColumnProvider(DefaultColumnProviders.Instance)
                .AddExporter(new JsonExporter());
            BenchmarkRunner.Run<AverageBlogRanking>(config);
            return Ok();
        }

        //modeling for performance
        //1 denormalization  
        //2 table per hierarchy (not table per type)
    }
}
