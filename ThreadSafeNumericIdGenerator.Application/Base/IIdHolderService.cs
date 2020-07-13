using System.Threading.Tasks;

namespace ThreadSafeNumericIdGenerator.Application.Base
{
    public interface IIdHolderService
    {
        Task<long> Next(string name);
    }
}
