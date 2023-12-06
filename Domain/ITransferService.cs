public interface ITransferService
{
    Exception? Transfer(TransferRequest transferRequest, DateTime dateTime);
}