using CqrsTemplate.Domain.Common;
using System;
using System.Threading.Tasks;

namespace CqrsTemplate.Application
{
    public class MessagesDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public MessagesDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync(ICommand command)
        {
            var commandHandlerType = typeof(ICommandHandler<>);
            Type[] commandHandlerTypesArguments = { command.GetType() };
            var commandHandlerGenericType = commandHandlerType.MakeGenericType(commandHandlerTypesArguments);
            dynamic handler = serviceProvider.GetService(commandHandlerGenericType);
            await handler.HandleAsync((dynamic)command);
        }
    }
}
