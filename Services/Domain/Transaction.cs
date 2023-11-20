public class Transaction
{
    class TransactionDraft
    {
        public Account CreditAccount { get; }
        public Account DebitAccount { get; }
        public decimal Amount { get; }
        public TransactionDraft(
            Account creditAccount,
            Account debitAccount,
            decimal amount)
        {
            CreditAccount = creditAccount;
            DebitAccount = debitAccount;
            Amount = amount;
        }
    }

    public string Id { get; }
    public DateTime Date { get; }
    public string Description { get; }
    public Transaction(string id,
                        DateTime date,
                        string description
                        )
    {
        Id = id;
        Date = date;
        Description = description;
    }
    TransactionDraft? draft = null;
    public void DraftTransfer(
        Account creditAccount,
        Account debitAccount,
        decimal amount)
    {
        draft = new TransactionDraft(creditAccount, debitAccount, amount);
    }
    public void CommitDraft()
    {
        if (draft is null) throw new InvalidOperationException("No drafts");
        draft.CreditAccount.Credit(draft.Amount);
        draft.DebitAccount.Debit(draft.Amount);
    }
    public void Transfer(Account creditAccount,
                        Account debitAccount,
                        decimal amount)
    {
        creditAccount.Credit(amount);
        debitAccount.Debit(amount);
    }
}