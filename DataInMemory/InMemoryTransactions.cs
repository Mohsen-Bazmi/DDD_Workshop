

using InternalMessaging;

public class InMemoryTransactions : Transactions
{
    readonly IMessageDispatcher messageDispatcher;

    public InMemoryTransactions(IMessageDispatcher messageDispatcher)
    => this.messageDispatcher = messageDispatcher;


    public List<Transaction> records { get; set; } = new();
    public void Add(Transaction transaction)
    {
        DispatchEventsOf(transaction);
        records.Add(transaction);
        //SaveChanges
    }

    public Transaction? FindById(TransactionId id)
    => All().FirstOrDefault(tx => tx.Id.Id == id.Id);

    public IEnumerable<Transaction> All()
    => records;

    public void Update(Transaction transaction)
    {
        DispatchEventsOf(transaction);
    }

    public void DispatchEventsOf(Transaction transaction)
    {
        messageDispatcher.Publish(transaction.NewEvents);
        transaction.ClearEvents();
    }
}


