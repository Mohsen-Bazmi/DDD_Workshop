public abstract class AggregateRoot
{
    protected readonly Queue<IDomainEvent> newEvents = new();
    public IEnumerable<IDomainEvent> NewEvents => newEvents;

    public void ClearEvents()
    => newEvents.Clear();

    protected void AppendEvent(IDomainEvent e)
    => newEvents.Enqueue(e);
}