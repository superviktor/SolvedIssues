using CqrsTemplate.DataContracts;
using CqrsTemplate.Domain.Common;
using System.Collections.Generic;

namespace CqrsTemplate.Domain.Queries
{
    public class GetAllModelsQuery : IQuery<IEnumerable<ModelDto>>
    {
    }
}
