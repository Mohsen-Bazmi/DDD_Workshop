public class Transaction
{
    public string Id { get; }
    public DateTime Date { get; }
    public Account CreditAccount { get; }
    public Account DebitAccount { get; }
    public string Description { get; }
    public Transaction(string id,
                        DateTime date,
                        string description,
                        Account creditAccount,
                        Account debitAccount)
    {
        Id = id;
        Date = date;
        Description = description;
        CreditAccount = creditAccount;
        DebitAccount = debitAccount;
    }
}