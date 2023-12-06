public class TransferService : ITransferService
{
    Accounts accounts;

    public TransferService(Accounts accounts)
    {
        //var (result,error) = Dox();
        //var (result,error) = DoY(65, 2df5, 66d, result);
        //var (result,error) = DoZ(65, 2df5, 66d, result);

        //DoX()
        //.Then(DoY(65, 2df5, 66d))
        //.Then(result => DoZ(65, 2df5, 66d, result))

        this.accounts = accounts;
    }

    public Exception? Transfer(TransferRequest transferRequest, DateTime dateTime)
    {
        var creditAccountId = transferRequest.Parties.CreditAccountId;
        var debitAccountId = transferRequest.Parties.DebitAccountId;
        var amount = transferRequest.Amount;

        var creditAccount = accounts.FindById(creditAccountId);
        var debitAccount = accounts.FindById(debitAccountId);

        if (debitAccount is null)
        {
            var (money, er) = Money.Create(0);
            if (money is null) return er;
            debitAccount = new Account(debitAccountId, money);
            accounts.Add(debitAccount);
        }

        if (creditAccount is null) throw new InvalidOperationException($"Credit account with the id '{creditAccountId}' not found.");
        // if(debitAccount is null) throw new InvalidOperationException($"Debit account with the id '{debitAccountId}' not found.");

        var error = creditAccount.Credit(amount);
        if (error is not null) return error;
        error = debitAccount.Debit(amount);
        if (error is not null) return error;

        accounts.Update(creditAccount);
        accounts.Update(debitAccount);

        return null;


    }
}