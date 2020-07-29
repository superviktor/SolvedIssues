using CqrsTemplate.Application.Common;
using CqrsTemplate.Domain.Common;
using System;
using System.Threading.Tasks;

namespace CqrsTemplate.Application.Decorators
{
    public class DataStoreRetryDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        private const int RetryAttempts = 3;

        private readonly ICommandHandler<TCommand> handler;

        public DataStoreRetryDecorator(ICommandHandler<TCommand> handler)
        {
            this.handler = handler;
        }

        public async Task HandleAsync(TCommand command)
        {
            for (int i = 0; ; i++)
            {
                try
                {
                    await handler.HandleAsync(command);
                    return;
                }
                catch (Exception exception)
                {
                    if (i >= RetryAttempts || !IsDataStoreException(exception))
                        throw;
                }
            }
        }

        private bool IsDataStoreException(Exception exception)
        {
            return false;
        }
    }
}