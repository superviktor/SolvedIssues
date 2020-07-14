using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Domain.Model.Entities;

namespace ThreadSafeNumericIdGenerator.Domain.Repository
{
    public interface IIdHolderRepository
    {
        Task CreateAsync(IdHolder idHolder);
    }
}
