public class TransferRequest
{
    public TransactionParties Parties { get; }
    public Money Amount { get; }
    public TransferRequest(TransactionParties parties, Money amount)
    {
        Parties = parties;
        Amount = amount;
    }
}