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

    public void DraftTransfer(DraftTransferCommand command)
    {
        var parties = new TransactionParties(command.CreditAccountId, command.DebitAccountId);
        var request = new TransferRequest(parties, command.Amount);
        var draft = Transaction.Draft(command.TransactionId, request);
        transactions.Add(draft);
    }

    public void CommitTransfer(CommitTransferCommand command)
    {
        var draft = transactions.FindById(command.TransactionId);

        if (draft is null) throw new InvalidOperationException($"No transaction drafts with the id: {command.TransactionId}");

        draft.Commit(dateTimeService.Now, transferService);

        transactions.Update(draft);
    }
}
