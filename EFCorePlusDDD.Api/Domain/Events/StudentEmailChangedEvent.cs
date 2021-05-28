using EFCorePlusDDD.Api.Domain.Models;

namespace EFCorePlusDDD.Api.Domain.Events
{
    public class StudentEmailChangedEvent : IDomainEvent
    {
        public StudentEmailChangedEvent(long studentId, Email email)
        {
            StudentId = studentId;
            Email = email;
        }

        public long StudentId { get; }
        public Email Email { get; }

    }
}