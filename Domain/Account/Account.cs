public class Account
{
    public Account(AccountId id, Money initialBalance)
    {
        Id = id;
        Balance = initialBalance;
    }
    public AccountId Id { get; }
    public Money Balance { get; private set; }

    public Exception? Credit(Money amount)
    {
        if (Balance <= amount) throw new NotEnoughChargeException();

        var (balance, error) = Balance - amount;

        if (balance is null) return error;

        Balance = balance;

        return null;
    }

    public Exception? Debit(Money amount)
    {
        var (balance, error) = Balance + amount;
        
        if (balance is null) return error;

        Balance = balance;

        return null;
    }
}