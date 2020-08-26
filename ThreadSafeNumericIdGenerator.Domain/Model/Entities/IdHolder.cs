using ThreadSafeNumericIdGenerator.Domain.Model.ValueObjects;

namespace ThreadSafeNumericIdGenerator.Domain.Model.Entities
{
    public class IdHolder
    {
        public IdHolderName Name { get; private set; }
        public IdHolderCurrentId CurrentId { get; private set; }

        public IdHolder(IdHolderName idHolderName, IdHolderCurrentId idHolderCurrentId)
        {
            Name = idHolderName;
            CurrentId = idHolderCurrentId;
        }

        public long Next()
        {
            return CurrentId.Next();
        } 
    }
}
