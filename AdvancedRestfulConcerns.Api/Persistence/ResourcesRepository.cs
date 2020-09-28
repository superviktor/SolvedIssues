using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedRestfulConcerns.Api.Contract;
using AdvancedRestfulConcerns.Api.Helpers;
using AdvancedRestfulConcerns.Api.Model;

namespace AdvancedRestfulConcerns.Api.Persistence
{
    public class ResourcesRepository : IResourceRepository
    {
        private readonly IPropertyMappingService propertyMappingService;

        public ResourcesRepository(IPropertyMappingService propertyMappingService)
        {
            this.propertyMappingService = propertyMappingService;
        }

        private List<Resource> resources = new List<Resource>
        {
            new Resource(Guid.Parse("d71032b3-0129-4cbb-a0ee-b44c26b77f12"),  "aaa", "zzz", DateTime.Now.AddYears(-3), null),
            new Resource(Guid.Parse("072c617c-0ff1-479c-bbd7-5b8e442cb3ec"),"yyy", "ccc", DateTime.Now.AddYears(-20), null),
            new Resource(Guid.Parse("4bc13310-094c-415e-8784-d91efe8671c7"),"bbb", "xxx", DateTime.Now.AddYears(-50), null),
            new Resource(Guid.Parse("54c23c10-4b09-4b9c-9f66-cd837e9e9f00"),"mmm", "kkk", DateTime.Now.AddYears(-12), null),
            new Resource(Guid.Parse("1de98232-a674-4935-96c6-ca4ed5128be1"),"lll", "qqq", DateTime.Now.AddYears(-16), null),
            new Resource(Guid.Parse("773b1e96-5f83-472d-b09e-cd920c71261c"),"rrr", "ttt", DateTime.Now.AddYears(-22), null),
            new Resource(Guid.Parse("da81a58c-750d-4e46-8324-6317b02e390f"),"aaa", "bbb", DateTime.Now.AddYears(-7), null),
            new Resource(Guid.Parse("5302d6f3-e3fc-40df-b960-2cfee76f7f67"),"aaa", "bbb", DateTime.Now.AddYears(-19), null)
        };

        public IQueryable<Resource> Resources => resources.AsQueryable();

        public PagedList<Resource> GetAll(int pageNumber, int pageSize, string orderBy)
        {
            var resourceMappingDictionary = propertyMappingService.GetPropertyMapping<Resource, ResourceDto>();
            var result = Resources.ApplySort(orderBy, resourceMappingDictionary).ToList();

            return PagedList<Resource>.Create(result.AsQueryable(), pageNumber, pageSize);
        }

        public Resource GetAll(Guid id)
        {
           return resources.SingleOrDefault(r => r.Id == id);
        }

        public void Create(Resource resource)
        {
            resources.Add(resource);
        }
    }
}