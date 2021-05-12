using System;

namespace EmitHandleDomainEvents.Domain
{
    public class BacklogItem : Entity
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public virtual Sprint Sprint { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        private BacklogItem() { }

        public BacklogItem(string desc)
        {
            this.Id = NewIdGuid();
            this.Description = desc;
        }

        public void CommitTo(Sprint s)
        {
            this.Sprint = s;
            this.Publish(new BacklogItemCommitted(this, s));
        }
    }
}