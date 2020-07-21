using CqrsTemplate.DataContracts;
using CqrsTemplate.Domain.CommandHandlers;
using CqrsTemplate.Domain.Commands;
using CqrsTemplate.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CqrsTemplate.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelController : ControllerBase
    {
        private readonly IModelRepository repository;

        public ModelController(IModelRepository repository)
        {
            this.repository = repository;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModel(Guid id, [FromBody] UpdateModelDto updateModelDto)
        {
            var command = new UpdateModelCommand
            {
                Id = id,
                Name = updateModelDto.Name
            };

            var handler = new UpdateModelCommandHandler(repository);
            await handler.HandleAsync(command);

            return Ok();
        }
    }
}
