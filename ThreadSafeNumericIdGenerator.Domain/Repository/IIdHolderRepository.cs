using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Repository.DataContract;

namespace ThreadSafeNumericIdGenerator.Domain.Repository
{
    public interface IIdHolderRepository
    {
        Task CreateAsync(IdHolderTableEntity idHolder);
        Task<bool> ExistsAsync(string name);
        Task<IdHolderTableEntity> GetAsync(string name);
        Task UpdateAsync(IdHolderTableEntity idHolder);
    }
}
