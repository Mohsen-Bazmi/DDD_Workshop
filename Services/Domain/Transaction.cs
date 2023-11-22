public class Transaction
{
    class Transfer
    {
        public string CreditAccountId { get; }
        public string DebitAccountId { get; }
        public decimal Amount { get; }
        public Transfer(
            string creditAccountId,
            string debitAccountId,
            decimal amount)
        {
            CreditAccountId = creditAccountId;
            DebitAccountId = debitAccountId;
            Amount = amount;
        }
    }

    public string Id { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    protected Transaction(string id,
        DateTime date,
        string description,
        string creditAccountId,
        string debitAccountId,
        decimal amount)
    {
        Id = id;
        Date = date;
        Description = description;
        draft = new Transfer(creditAccountId, debitAccountId, amount);
    }
    Transfer? draft = null;
    public static Transaction Draft(
        string id,
        DateTime date,
        string description,
        string creditAccountId,
        string debitAccountId,
        decimal amount)
    => new Transaction(
        id,
        date,
        description,
        creditAccountId,
        debitAccountId,
        amount
    );
    public void Commit(ITransferService transferService)
    {
        if (draft is null) throw new InvalidOperationException("No drafts");
        transferService.Transfer(draft.CreditAccountId, draft.DebitAccountId, draft.Amount);
    }
}