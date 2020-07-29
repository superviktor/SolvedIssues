using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CqrsTemplate.Domain.Repository
{
    public interface IModelRepository
    {
        Task<IEnumerable<Model>> GetAllAsync();
        Task<Model> GetByIdAsync(Guid id);
        Task UpdateAsync(Model model);
    }
}
