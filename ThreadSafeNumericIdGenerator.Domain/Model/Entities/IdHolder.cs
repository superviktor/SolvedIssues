using ThreadSafeNumericIdGenerator.Domain.Base;
using ThreadSafeNumericIdGenerator.Domain.Model.ValueObjects;

namespace ThreadSafeNumericIdGenerator.Domain.Model.Entities
{
    public class IdHolder
    {
        public IdHolderName Name { get; private set; }
        public IdHolderCurrentId CurrentId { get; private set; }

        private IdHolder(IdHolderName idHolderName, IdHolderCurrentId idHolderCurrentId)
        {
            Name = idHolderName;
            CurrentId = idHolderCurrentId;
        }

        public static Result<IdHolder> Create(string name, long? startFrom = null)  
        {
            var idHolderName = IdHolderName.Create(name);
            var idHolderCurrentId = IdHolderCurrentId.Create(startFrom);
            var result = Result.Combine(idHolderName, idHolderCurrentId);

            return result.IsSuccess
                ? Result.Success(new IdHolder(idHolderName.Value, idHolderCurrentId.Value))
                : Result.Fail<IdHolder>(result.Error);
        }
    }
}
