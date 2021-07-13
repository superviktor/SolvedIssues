using System;
using System.Collections.Generic;

namespace DalPerformance.Api
{
    public class Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<SubEntity> SubEntities { get; set; }
    }
}
