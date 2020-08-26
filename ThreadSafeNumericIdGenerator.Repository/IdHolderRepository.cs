using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.AzureTablesRepository;
using ThreadSafeNumericIdGenerator.Domain.Repository;
using ThreadSafeNumericIdGenerator.Repository.DataContract;

namespace ThreadSafeNumericIdGenerator.Repository.Repository
{
    public class IdHolderRepository : IIdHolderRepository
    {
        private const string IdHolderTableName = "IdHolders";
        private const string IdHolcerPartitionKey = "IdHolder";
        private readonly IAzureTableRepository<IdHolderTableEntity> repository;

        public IdHolderRepository(IAzureTableRepository<IdHolderTableEntity> repository)
        {
            this.repository = repository;
        }

        public async Task CreateAsync(IdHolderTableEntity idHolder)
        {
            idHolder.PartitionKey = IdHolcerPartitionKey;
            idHolder.RowKey = idHolder.Name;

            await repository.AddAsync(IdHolderTableName, idHolder);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            var idHolder = await repository.GetAsync(IdHolderTableName, IdHolcerPartitionKey, name);

            return idHolder != null;
        }

        public async Task<IdHolderTableEntity> GetAsync(string name)
        {
            var idHolder = await repository.GetAsync(IdHolderTableName, IdHolcerPartitionKey, name);

            return idHolder;
        }

        public async Task UpdateAsync(IdHolderTableEntity idHolder)
        {
            idHolder.PartitionKey = IdHolcerPartitionKey;
            idHolder.RowKey = idHolder.Name;

            await repository.UpdateAsync(IdHolderTableName, idHolder);
        }
    }
}
