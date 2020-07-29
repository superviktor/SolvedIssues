using CqrsTemplate.Domain;
using CqrsTemplate.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CqrsTemplate.Repository
{
    public class ModelRepository : IModelRepository
    {
        public async Task<IEnumerable<Model>> GetAllAsync()
        {
            return await Task.FromResult(new List<Model>
            {
                Model.Create("name1"),
                Model.Create("name2")
            });
        }

        public Task<Model> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Model.Create("name"));
        }

        public Task UpdateAsync(Model model)
        {
            return Task.Delay(1);
        }
    }
}
