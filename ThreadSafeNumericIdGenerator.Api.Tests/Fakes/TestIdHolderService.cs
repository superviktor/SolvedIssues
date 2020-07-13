using System.Threading;
using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Application.Base;

namespace ThreadSafeNumericIdGenerator.Api.Tests.Fakes
{
    class TestIdHolderService : IIdHolderService
    {
        private static long currentId = 0;

        public Task<long> Next(string name)
        {
            Interlocked.Increment(ref currentId);
            return Task.FromResult(currentId);
        }
    }
}
