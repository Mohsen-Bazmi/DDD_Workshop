
public class Accounts
{
    readonly List<Account> _records = new List<Account>();

    public void Update(Account account)
    {
        var record = FindById(account.Id);
    }

    public Account? FindById(string id) => _records.FirstOrDefault(_ => _.Id == id);

    public void Add(Account account) => _records.Add(account);

    public void Clear() => _records.Clear();
}