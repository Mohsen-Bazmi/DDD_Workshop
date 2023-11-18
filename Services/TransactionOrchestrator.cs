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
            creditAccount = new Account(creditAccountId);
            accounts.Add(creditAccount);
        }
        

        
        var debitAccount = accounts.FindById(debitAccountId);

        if(debitAccount is null){
            debitAccount = new Account(debitAccountId);
            accounts.Add(debitAccount);
        }

        creditAccount.Credit(amount);
        debitAccount.Debit(amount);


        accounts.Update(creditAccount);
        accounts.Update(debitAccount);
        new Transactions().Add(new Transaction(
            Guid.NewGuid().ToString(),
            DateTime.Now,
            "",
            creditAccount,
            debitAccount
        ));

    }
}
