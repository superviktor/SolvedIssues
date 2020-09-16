using System.Collections.Generic;
using System.Linq;
using AdvancedRestfulConcerns.Api.Helpers;
using AdvancedRestfulConcerns.Api.Model;

namespace AdvancedRestfulConcerns.Api.Persistence
{
    public class ResourcesRepository : IResourceRepository
    {
        public PagedList<Resource> Get(int pageNumber, int pageSize)
        {
            var resources = new List<Resource>()
            {
                new Resource {Name = "name1"},
                new Resource {Name = "name2"},
                new Resource {Name = "name3"},
                new Resource {Name = "name4"},
                new Resource {Name = "name5"},
                new Resource {Name = "name6"},
                new Resource {Name = "name7"},
            }.AsQueryable();

            return PagedList<Resource>.Create(resources, pageNumber, pageSize);
        }
    }
}
