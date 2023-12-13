
public interface IHandleMessage<in TMessage> //where TMessage : IDomainEvent
{
    void Handle(TMessage message);
}