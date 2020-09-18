using System;
using AdvancedRestfulConcerns.Api.Helpers;
using AdvancedRestfulConcerns.Api.Model;

namespace AdvancedRestfulConcerns.Api.Persistence
{
    public interface IResourceRepository
    {
        PagedList<Resource> GetAll(int pageNumber, int pageSize, string orderBy);
        Resource GetAll(Guid id);
    }
}
