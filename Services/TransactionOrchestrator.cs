namespace Services;

public class TransactionOrchestrator
{
    readonly Transactions transactions;
    readonly ITransferService transferService;
    public TransactionOrchestrator(Transactions transactions, ITransferService transferService)
    {
        this.transactions = transactions;
        this.transferService = transferService;
    }

    public void DraftTransfer(TransactionId transactionId, AccountId creditAccountId, AccountId debitAccountId, Money amount, DateTime transactionDate)
    {
        transactions.Add(Transaction.Draft(transactionId, transactionDate, creditAccountId, debitAccountId, amount));
    }

    public void CommitTransfer(
        TransactionId transactionId)
    {
        var draft = transactions.FindById(transactionId);

        if(draft is null) throw new InvalidOperationException($"No transaction drafts with the id: {transactionId}");
        
        draft.Commit(transferService);

        transactions.Update(draft);
    }
}
