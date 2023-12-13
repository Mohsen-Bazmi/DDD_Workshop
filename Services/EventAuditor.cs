
using Services;

public class EventAuditor : IHandleMessage<TransactionCommited>, IHandleMessage<IDomainEvent>
{
    public EventAuditor(TransactionOrchestrator transactionOrchestrator)
    {
        if (transactionOrchestrator is null) throw new NullReferenceException("transactionOrchestrator is null");
    }
    public void Handle(TransactionCommited @event)
    {
        Console.WriteLine("Handler1");
        Console.WriteLine(@event);
    }
    // }
    // public class EventAuditor2 : IHandleMessage<IDomainEvent>
    // {
    public void Handle(IDomainEvent message)
    {
        Console.WriteLine("IDomainEvent Handler");
        Console.WriteLine(message);
    }
}