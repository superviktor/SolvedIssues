using System;

namespace DalPerformance.Api
{
    public class SubEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid EntityId { get; set; }
        public Entity Entity { get; set; }
    }
}
