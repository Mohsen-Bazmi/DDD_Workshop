
public class TransactionAccountDetail
{
    public TransactionAccountDetail(
        string creditAccountId,
        string debitAccountId,
        Money amount)
    {
        CreditAccountId = creditAccountId;
        DebitAccountId = debitAccountId;
        StopIfAmountBeLessThanZeroOrEquals(amount.Value);
        Amount = amount;
    }
    public  string CreditAccountId { get; private set; }
    public  string DebitAccountId { get; private set; }
    public  Money Amount { get; private set; }

    private static void StopIfAmountBeLessThanZeroOrEquals(decimal amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Transaction Amount Value Can Not Be Less Than Zero Or Equals");
    }
}
