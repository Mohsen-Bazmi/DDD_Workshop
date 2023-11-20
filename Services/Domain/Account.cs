public class Account
{
    public Account(string id, decimal initialBalance = 0)
    {
        if(initialBalance < 0) throw new InvalidOperationException("Accounts cannot be initialized with negative balance.");

        Id = id;
        Balance = initialBalance;
    }
    public string Id { get; }
    public decimal Balance { get; private set; }

    public void Credit(decimal amount)
    { 
        if (Balance <= amount)
            throw new InvalidOperationException("No enough charge");
        
        Balance -= amount;
    }

    public void Debit(decimal amount)
    {
        Balance += amount;       
    }


}