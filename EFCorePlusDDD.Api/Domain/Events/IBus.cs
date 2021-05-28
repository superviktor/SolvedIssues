namespace EFCorePlusDDD.Api.Domain.Events
{
    public interface IBus
    {
        void Send(string message);
    }
}