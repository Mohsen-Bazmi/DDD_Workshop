
public class InMemoryAccounts : Accounts
{

    readonly List<Account> records = new List<Account>();
    public Account? FindById(AccountId id)
    {
        return records.FirstOrDefault(a => a.Id.Id == id.Id);
    }

    public void Update(Account account)
    {
        var record = FindById(account.Id);

    }

    public void Add(Account account)
    {
        records.Add(account);
    }
}