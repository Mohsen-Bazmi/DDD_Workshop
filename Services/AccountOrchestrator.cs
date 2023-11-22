public class AccountOrchestrator
{
    private Accounts accounts;
    public AccountOrchestrator(Accounts accounts)
    {
        this.accounts = accounts;
    }
    public void OpenAccount(string accountId, decimal initialBalance)
    {
        accounts.Add(new Account(accountId,initialBalance));
    }
}