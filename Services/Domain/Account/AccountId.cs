public class AccountId
{
    public string Id { get; }
    public AccountId(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new InvalidOperationException("Account id cannot be null or empty");
        Id = id;
    }
    public static implicit operator AccountId(string id)
    => new AccountId(id);
}