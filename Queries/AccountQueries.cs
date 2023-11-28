public class AccountQueries
{
    Accounts _accounts;
    public AccountQueries(Accounts accounts)
    => _accounts = accounts;
    public BalanceViewModel? GetBalanceForAccount(string accountId)
    {
        var account= _accounts.FindById(accountId);
        if(account is null) return null;
        return new BalanceViewModel(Id: account.Id, Balance:account.Balance.Value);
    }
}