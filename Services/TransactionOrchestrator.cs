namespace Services;

public class TransactionOrchestrator
{
    readonly Transactions _transactions;
    readonly ITransferService _transferService;
    public TransactionOrchestrator(
        Transactions transactions, 
        ITransferService transferService)
    {
        _transactions = transactions;
        _transferService = transferService;
    }

    public void DraftTransfer(
        string transactionId,
        TransactionAccountDetail accountDetail,
        DateTime transactionDate,
        string description)
    {
        _transactions.Add(
            Transaction.Draft(transactionId, transactionDate, description, accountDetail));
    }

    public void CommitTransfer(
        string transactionId)
    {
        var draft = _transactions.FindById(transactionId);
        StopIfNoTransactionDraftExist(transactionId, draft);

        draft!.Commit(_transferService);

        _transactions.Update(draft);
    }

    private static void StopIfNoTransactionDraftExist(
        string transactionId,
        Transaction? draft)
    {
        if (draft is null)
            throw new InvalidOperationException($"No transaction drafts with the id: {transactionId}");
    }
}
