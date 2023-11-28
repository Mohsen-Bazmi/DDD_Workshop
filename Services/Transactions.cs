

public class Transactions
{
    public List<Transaction> _records { get; set; } = new();
    public void Add(Transaction transaction)
    => _records.Add(transaction);

    public Transaction? FindById(string id)
    => _records.FirstOrDefault(tx => tx.Id == id);

    public IEnumerable<Transaction> All()
    => _records;

    public void Update(Transaction draft)
    {
        _records.RemoveAll(_=>_.Id == draft.Id);
        _records.Add(draft);
    }
}