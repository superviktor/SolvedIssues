using CqrsTemplate.Domain.Common;
using System.Threading.Tasks;

namespace CqrsTemplate.Application.Common
{
    public interface IQueryHandler<TInput, TOutput> where TInput : IQuery<TOutput>
    {
        Task<TOutput> HandleAsync(TInput query);
    }
}
