using AdvancedRestfulConcerns.Api.Helpers;
using AdvancedRestfulConcerns.Api.Model;

namespace AdvancedRestfulConcerns.Api.Persistence
{
    public interface IResourceRepository
    {
        PagedList<Resource> Get(int pageNumber, int pageSize);
    }
}
