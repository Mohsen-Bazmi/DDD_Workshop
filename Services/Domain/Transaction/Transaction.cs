
public class Transaction
{
    public AccountId CreditAccountId { get; }
    public AccountId DebitAccountId { get; }
    public Money Amount { get; }

    public TransactionId Id { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    public TransferStatus Status { get; private set; } = TransferStatus.Draft;

    protected Transaction(TransactionId id,
        DateTime date,
        AccountId creditAccountId,
        AccountId debitAccountId,
        Money amount)
    {
        Id = id;
        Date = date;

        CreditAccountId = creditAccountId;
        DebitAccountId = debitAccountId;
        Amount = amount;
    }

    public void Describe(string description)
    => Description = description;


    public static Transaction Draft(
        TransactionId id,
        DateTime date,
        AccountId creditAccountId,
        AccountId debitAccountId,
        Money amount)
    => new Transaction(
        id,
        date,
        creditAccountId,
        debitAccountId,
        amount
    );

    public void Commit(ITransferService transferService)
    {
        transferService.Transfer(CreditAccountId, DebitAccountId, Amount);
        Status = TransferStatus.Commit;
    }
}