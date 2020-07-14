using Microsoft.Azure.Cosmos.Table;

namespace ThreadSafeNumericIdGenerator.Repository.Model
{
    public class IdHolder : TableEntity
    {
        public string Name { get; set; }
        public long CurrentId { get; set; }
    }
}
