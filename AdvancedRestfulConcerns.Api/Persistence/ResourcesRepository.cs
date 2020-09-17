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

        public PagedList<Resource> Get(int pageNumber, int pageSize, string orderBy)
        {
            var resources = new List<Resource>
            {
                new Resource("aaa", "zzz", DateTime.Now.AddYears(-3)),
                new Resource("yyy", "ccc", DateTime.Now.AddYears(-20)),
                new Resource("bbb", "xxx", DateTime.Now.AddYears(-50)),
                new Resource("mmm", "kkk", DateTime.Now.AddYears(-12)),
                new Resource("lll", "qqq", DateTime.Now.AddYears(-16)),
                new Resource("rrr", "ttt", DateTime.Now.AddYears(-22)),
                new Resource("aaa", "bbb", DateTime.Now.AddYears(-7)),
                new Resource("aaa", "bbb", DateTime.Now.AddYears(-19))
            }.AsQueryable();

            var resourceMappingDictionary = propertyMappingService.GetPropertyMapping<Resource, ResourceDto>();
            resources = resources.ApplySort(orderBy, resourceMappingDictionary);

            return PagedList<Resource>.Create(resources, pageNumber, pageSize);
        }
    }
}