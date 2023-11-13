public class Transactions
{
    public static List<Transaction> records { get; set; } = new();
    public void Add(Transaction transaction)
    {
        records.Add(transaction);
    }
}