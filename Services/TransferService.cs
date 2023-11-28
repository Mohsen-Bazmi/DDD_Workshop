public class TransferService : ITransferService
{
    Accounts _accounts;

    public TransferService(Accounts accounts)
    {
        _accounts = accounts;
    }

    public void Transfer(TransactionAccountDetail accountDetail)
    {
        var creditAccount = _accounts.FindById(accountDetail.CreditAccountId);
        StopIfCreditAcccountNotExist(accountDetail, creditAccount);
        creditAccount!.Credit(accountDetail.Amount);
        _accounts.Update(creditAccount);

        var debitAccount = _accounts.FindById(accountDetail.DebitAccountId);
        debitAccount = CreateDebitAccountIfNotExist(accountDetail, debitAccount);
        debitAccount!.Debit(accountDetail.Amount);
        _accounts.Update(debitAccount!);
    }

    private static void StopIfCreditAcccountNotExist(
        TransactionAccountDetail accountDetail,
        Account? creditAccount)
    {
        if (creditAccount is null)
            throw new InvalidOperationException($"Credit account with the id '{accountDetail.CreditAccountId}' not found.");
    }

    private Account CreateDebitAccountIfNotExist(
        TransactionAccountDetail accountDetail,
        Account? debitAccount)
    {
        if (debitAccount is null)
        {
            var newDebitAccount = new Account(accountDetail.DebitAccountId, 0);
            _accounts.Add(newDebitAccount!);
            return newDebitAccount;
        }
        return debitAccount;
    }
}