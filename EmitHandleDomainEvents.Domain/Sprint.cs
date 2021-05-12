using System;
using System.Collections.Generic;

namespace EmitHandleDomainEvents.Domain
{
    public class Sprint: Entity
    {
        public Guid Id { get; private set; }
        public DateTime StartedAt { get; private set; }
        public DateTime EndedAt { get; private set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<BacklogItem> BacklogItems { get; private set; } = new List<BacklogItem>();

        private Sprint(){}

        public Sprint(DateTime startDate, DateTime endDate)
        {
            this.Id = NewIdGuid();
            this.StartedAt = startDate;
            this.EndedAt = endDate;
        }

        public void AddBacklogItem(BacklogItem backlogItem)
        {
            this.BacklogItems.Add(backlogItem);
        }
    }
}