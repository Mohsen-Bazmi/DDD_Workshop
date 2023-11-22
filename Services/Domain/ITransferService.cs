public interface ITransferService
{
    void Transfer(string creditAccountId, string debitAccountId, decimal amount);
}