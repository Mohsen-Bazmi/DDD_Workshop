using FunctionalLibrary;

public class AccountOrchestrator
{
    private Accounts accounts;
    public AccountOrchestrator(Accounts accounts)
    {
        this.accounts = accounts;
    }
    public Exception? OpenAccount(string accountId, decimal initialBalance)
    => Money.Create(initialBalance)
            .Select(l => accounts.Add(new Account(accountId, l)));

}