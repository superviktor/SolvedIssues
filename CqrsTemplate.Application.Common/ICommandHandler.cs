using CqrsTemplate.Domain.Common;
using System.Threading.Tasks;

namespace CqrsTemplate.Application.Common
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
