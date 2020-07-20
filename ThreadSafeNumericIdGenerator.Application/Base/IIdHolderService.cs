using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Common;
using ThreadSafeNumericIdGenerator.DataContract;

namespace ThreadSafeNumericIdGenerator.Application.Base
{
    public interface IIdHolderService
    {
        Task<Result<long>> NextAsync(string name);
        Task<Result> CreateAsync(CreateIdHolderDto createIdHolderDto);
        Task<bool> ExistsAsync(string name);
    }
}
