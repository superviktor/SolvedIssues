using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Domain.Model.Entities;

namespace ThreadSafeNumericIdGenerator.Domain.Repository
{
    public interface IIdHolderRepository
    {
        // shoud here be entity not domain model
        Task CreateAsync(IdHolder idHolder);
    }
}
