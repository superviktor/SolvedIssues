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
            return Ok(_context.Entities.TagWith("GET ALL"));
        }

        [HttpGet("id/{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_context.Entities.SingleOrDefault(x=>x.Id == id));
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
    }
}
