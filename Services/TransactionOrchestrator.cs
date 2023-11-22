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

    public void Transfer(
        string creditAccountId,
        string debitAccountId,
        decimal amount)
    {
        var transaction = Transaction.Draft(
            Guid.NewGuid().ToString(),
            DateTime.Now,
            "Salary",
            creditAccountId,
            debitAccountId,
            amount);

        transaction.Commit(transferService);

        transactions.Add(transaction);

    }
}
