namespace Services;

public class TransactionOrchestrator
{
    readonly Accounts accounts;
    public TransactionOrchestrator(Accounts accounts)
    {
        this.accounts = accounts;
    }

    public void Transfer(
        string creditAccountId,
        string debitAccountId,
        decimal amount)
    {
        var creditAccount = accounts.FindById(creditAccountId);
        if(creditAccount is null){
            creditAccount = new Account();
            creditAccount.Id = creditAccountId;
            accounts.Add(creditAccount);
        }
        

        
        var debitAccount = accounts.FindById(debitAccountId);

        if(debitAccount is null){
            debitAccount = new Account();
            debitAccount.Id = debitAccountId;

            accounts.Add(debitAccount);
        }

        if (creditAccount.Balance <= amount)
        {
            throw new InvalidOperationException("No enough charge");
        }
        creditAccount.Balance -= amount;
        debitAccount.Balance += amount;


        accounts.Update(creditAccount);
        accounts.Update(debitAccount);
        var transactions = new Transactions();
        transactions.Add(new Transaction{
            CreaditAccount = creditAccount,
            DebitAccount = debitAccount,
            Date = DateTime.Now,
            Description = "",
            Id = Guid.NewGuid().ToString()
        });

    }
}
