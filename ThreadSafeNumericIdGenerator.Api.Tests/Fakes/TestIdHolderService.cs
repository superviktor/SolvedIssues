using System.Threading;
using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Application.Base;
using ThreadSafeNumericIdGenerator.DataContract;

namespace ThreadSafeNumericIdGenerator.Api.Tests.Fakes
{
    class TestIdHolderService : IIdHolderService
    {
        private static long currentId = 0;

        public Task CreateAsync(CreateIdHolderDto createIdHolderDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistsAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<long> NextAsync(string name)
        {
            Interlocked.Increment(ref currentId);
            return Task.FromResult(currentId);
        }
    }
}
