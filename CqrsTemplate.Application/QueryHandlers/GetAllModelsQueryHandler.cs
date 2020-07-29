using CqrsTemplate.Application.Common;
using CqrsTemplate.DataContracts;
using CqrsTemplate.Domain.Queries;
using CqrsTemplate.Domain.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CqrsTemplate.Application.QueryHandlers
{
    public class GetAllModelsQueryHandler : IQueryHandler<GetAllModelsQuery, IEnumerable<ModelDto>>
    {
        private readonly IModelRepository repository;

        public GetAllModelsQueryHandler(IModelRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<ModelDto>> HandleAsync(GetAllModelsQuery query)
        {
            var models = await repository.GetAllAsync();

            return models.Select(m => new ModelDto { Name = m.Name });
        }
    }
}
