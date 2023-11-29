public interface ITransferService
{
    void Transfer(AccountId creditAccountId, AccountId debitAccountId, Money amount);
}