public class Transaction
{
    class Transfer
    {
        public Account CreditAccount { get; }
        public Account DebitAccount { get; }
        public decimal Amount { get; }
        public Transfer(
            Account creditAccount,
            Account debitAccount,
            decimal amount)
        {
            CreditAccount = creditAccount;
            DebitAccount = debitAccount;
            Amount = amount;
        }

        public void Commit()
        {
            CreditAccount.Credit(Amount);
            DebitAccount.Debit(Amount);
        }
    }

    public string Id { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    protected Transaction(string id,
        DateTime date,
        string description,
        Account creditAccount,
        Account debitAccount,
        decimal amount)
    {
        Id = id;
        Date = date;
        Description = description;
        draft = new Transfer(creditAccount, debitAccount, amount);
    }
    Transfer? draft = null;
    public static Transaction Draft(
        string id,
        DateTime date,
        string description,
        Account creditAccount,
        Account debitAccount,
        decimal amount)
    => new Transaction(
        id,
        date,
        description,
        creditAccount,
        debitAccount,
        amount
    );
    public void Commit()
    {
        if (draft is null) throw new InvalidOperationException("No drafts");
        draft.Commit();
    }
}