public class AccountOrchestrator
{
    private Accounts _accounts;
    public AccountOrchestrator(Accounts accounts)
    {
        _accounts = accounts;
    }
    public void OpenAccount(
        string accountId,
        decimal initialBalance)
    {
        _accounts.Add(new Account(accountId, initialBalance));
    }
}