using CqrsTemplate.Domain;
using CqrsTemplate.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CqrsTemplate.Repository
{
    public class ModelRepository : IModelRepository
    {
        public Task<IEnumerable<Model>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Model> GetByIdAsync(Guid id)
        {
            return Task.FromResult(new Model());
        }

        public Task UpdateAsync(Model model)
        {
            return Task.Delay(1);
        }
    }
}
