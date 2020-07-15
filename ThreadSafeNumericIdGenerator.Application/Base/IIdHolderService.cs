using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.DataContract;

namespace ThreadSafeNumericIdGenerator.Application.Base
{
    public interface IIdHolderService
    {
        Task<long> NextAsync(string name);
        Task CreateAsync(CreateIdHolderDto createIdHolderDto);
    }
}
