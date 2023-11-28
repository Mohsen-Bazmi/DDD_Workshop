
public enum TransferStatus
{
    Commit,
    Draft,
}

public class Transaction
{
    public string Id { get; private set; }
    public string CreditAccountId { get; }
    public string DebitAccountId { get; }
    public Money Amount { get; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    public TransferStatus Status { get; private set; } = TransferStatus.Draft;

    protected Transaction(string id,
        DateTime date,
        string description,
        TransactionAccountDetail accountDetail)
    {
        Id = id;
        Date = date;
        Description = description;
        CreditAccountId = accountDetail.CreditAccountId;
        DebitAccountId = accountDetail.DebitAccountId;
        Amount = accountDetail.Amount;
    }

    public static Transaction Draft(
        string id,
        DateTime date,
        string description,
        TransactionAccountDetail accountDetail)
    => new Transaction(
        id,
        date,
        description,
        accountDetail);

    public void Commit(ITransferService transferService)
    {
        var TransactionDetail =
            new TransactionAccountDetail(
                CreditAccountId,
                DebitAccountId,
                Amount);

        transferService
            .Transfer(TransactionDetail);

        Status = TransferStatus.Commit;
    }
}