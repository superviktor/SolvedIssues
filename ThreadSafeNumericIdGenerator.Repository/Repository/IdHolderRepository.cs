using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Domain.Repository;
using ThreadSafeNumericIdGenerator.Repository.Base;
using ThreadSafeNumericIdGenerator.Repository.Model;

namespace ThreadSafeNumericIdGenerator.Repository.Repository
{
    public class IdHolderRepository : IIdHolderRepository
    {
        private const string IdHolderTableName = "IdHolders";
        private const string IdHolcerPartitionKey = "IdHolder";
        private readonly IAzureTableRepository<IdHolder> repository;

        public IdHolderRepository(IAzureTableRepository<IdHolder> repository)
        {
            this.repository = repository;
        }

        public async Task CreateAsync(Domain.Model.Entities.IdHolder idHolder)
        {
            IdHolder entity = new IdHolder
            {
                PartitionKey = IdHolcerPartitionKey,
                RowKey = idHolder.Name,
                Name = idHolder.Name,
                CurrentId = idHolder.CurrentId
            };

            await repository.AddAsync(IdHolderTableName, entity);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            var idHolder = await repository.GetAsync(IdHolderTableName, IdHolcerPartitionKey, name);

            return idHolder != null;
        }
    }
}
