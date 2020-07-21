using System.Threading.Tasks;

namespace CqrsTemplate.Domain.Common
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
