using System;

namespace EmitHandleDomainEvents.Domain
{
    public class BacklogItemCommitted : IDomainEvent
    {
        public Guid BacklogItemId { get; set; }
        public Guid SprintId { get; set; }
        public DateTime CreatedAt { get; set; }

        private BacklogItemCommitted() { }

        public BacklogItemCommitted(BacklogItem backlogItem, Sprint sprint)
        {
            this.BacklogItemId = backlogItem.Id;
            this.SprintId = sprint.Id;
            this.CreatedAt = backlogItem.CreatedAt;
        }
    }
}