namespace ThreadSafeNumericIdGenerator.Domain.Model
{
    public class IdHolder
    {
        public string Name { get; private set; }
        public long CurrentId { get; private set; }

        private IdHolder()
        {
        }

        public static IdHolder Create(string name, long? startFrom = null)
        {
            //validate
            return new IdHolder
            {
                Name = name,
                CurrentId = startFrom ?? 0
            };
        }
    }
}
