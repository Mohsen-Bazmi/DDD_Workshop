public class TransactionId
{
    public string Id { get; }
    public TransactionId(string id)
    {
        if(string.IsNullOrEmpty(id)) throw new InvalidOperationException("Transaction id cannot be null or empty");
        Id = id;
    }
}