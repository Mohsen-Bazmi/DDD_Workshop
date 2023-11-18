public class Transaction
{
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

    public void Transfer(Account creditAccount,
                        Account debitAccount,
                        decimal amount)
    {
        creditAccount.Credit(amount);
        debitAccount.Debit(amount);
    }
}