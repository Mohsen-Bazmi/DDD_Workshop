
public class Transaction
{
    public TransferRequest TransferRequest { get; }
    Queue<object> newEvents = new();
    public IEnumerable<object> NewEvents => newEvents;


    public AccountId CreditAccountId { get; }
    public AccountId DebitAccountId { get; }
    public Money Amount { get; }

    public TransactionId Id { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public TransferStatus Status { get; private set; } = TransferStatus.Draft;

    protected Transaction(TransactionId id,
        TransferRequest transferRequest)
    {
        Id = id;
        TransferRequest = transferRequest;
    }

    public void Describe(string description)
    => Description = description;


    public static Transaction Draft(
        TransactionId id,
        TransferRequest transferRequest)
    => new Transaction(
        id, transferRequest
    );

    public void Commit(DateTime dateTime, ITransferService transferService)
    {
        transferService.Transfer(TransferRequest, dateTime);
        Status = TransferStatus.Commit;
        
        newEvents.Enqueue(new {
            CreditAccountId, DebitAccountId, Amount
        });
    }
}