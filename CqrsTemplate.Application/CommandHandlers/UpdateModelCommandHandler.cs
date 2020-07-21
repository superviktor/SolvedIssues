using CqrsTemplate.Domain.Commands;
using CqrsTemplate.Domain.Common;
using CqrsTemplate.Domain.Repository;
using System;
using System.Threading.Tasks;

namespace CqrsTemplate.Domain.CommandHandlers
{
    public class UpdateModelCommandHandler : ICommandHandler<UpdateModelCommand>
    {
        private readonly IModelRepository repository;

        public UpdateModelCommandHandler(IModelRepository repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(UpdateModelCommand command)
        {
            var model = await repository.GetByIdAsync(command.Id);
            if (model == null)
                throw new Exception($"Model with id = {command.Id} can't be found");

            model.Update(command.Name);

            await repository.UpdateAsync(model);
        }
    }
}
