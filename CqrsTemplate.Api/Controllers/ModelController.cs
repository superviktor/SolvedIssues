using CqrsTemplate.Application;
using CqrsTemplate.DataContracts;
using CqrsTemplate.Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CqrsTemplate.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelController : ControllerBase
    {
        private readonly MessagesDispatcher messagesDispatcher;
        public ModelController( MessagesDispatcher messagesDispatcher)
        {
            this.messagesDispatcher = messagesDispatcher;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModel(Guid id, [FromBody] UpdateModelDto updateModelDto)
        {
            var command = new UpdateModelCommand
            {
                Id = id,
                Name = updateModelDto.Name
            };
            try
            {
                await messagesDispatcher.DispatchAsync(command);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }
    }
}
