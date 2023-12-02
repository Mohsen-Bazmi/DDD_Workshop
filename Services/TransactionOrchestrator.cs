namespace Services;

public class TransactionOrchestrator
{
    readonly Transactions transactions;
    readonly ITransferService transferService;
    readonly DateTimeService dateTimeService;
    public TransactionOrchestrator(Transactions transactions, ITransferService transferService, DateTimeService dateTimeService)
    {
        this.transactions = transactions;
        this.transferService = transferService;
        this.dateTimeService = dateTimeService;
    }

    public void DraftTransfer(string transactionId, string creditAccountId, string debitAccountId, decimal amount)
    {
        var parties = new TransactionParties(creditAccountId, debitAccountId);
        var request = new TransferRequest(parties, amount);
        var draft = Transaction.Draft(transactionId, request);
        transactions.Add(draft);
    }

    public void CommitTransfer(
        string transactionId)
    {
        var draft = transactions.FindById(transactionId);

        if (draft is null) throw new InvalidOperationException($"No transaction drafts with the id: {transactionId}");

        draft.Commit(dateTimeService.Now, transferService);

        transactions.Update(draft);
    }
}
