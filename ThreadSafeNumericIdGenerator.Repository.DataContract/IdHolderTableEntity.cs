using Microsoft.Azure.Cosmos.Table;

namespace ThreadSafeNumericIdGenerator.Repository.DataContract
{
    public class IdHolderTableEntity : TableEntity
    {
        public string Name { get; set; }
        public long CurrentId { get; set; }
    }
}
