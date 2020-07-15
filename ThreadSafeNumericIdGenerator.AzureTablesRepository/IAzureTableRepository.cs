using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThreadSafeNumericIdGenerator.AzureTablesRepository
{
    public interface IAzureTableRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(string tableName);
        Task<IEnumerable<T>> QueryAsync(string tableName, TableQuery<T> query);
        Task<T> GetAsync(string tableName, string partitionKey, string rowKey);
        Task<object> AddOrUpdateAsync(string tableName, ITableEntity entity);
        Task<object> DeleteAsync(string tableName, ITableEntity entity);
        Task<object> AddAsync(string tableName, ITableEntity entity);
        Task<IEnumerable<T>> AddBatchAsync(string tableName, IEnumerable<ITableEntity> entities, BatchOperationOptions options);
        Task<object> UpdateAsync(string tableName, ITableEntity entity);
    }
}
