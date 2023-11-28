
public class TransactionAccountDetail
{
    public TransactionAccountDetail(
        string creditAccountId,
        string debitAccountId,
        Money amount)
    {
        CreditAccountId = creditAccountId;
        DebitAccountId = debitAccountId;
        Amount = amount;
    }
    public  string CreditAccountId { get; private set; }
    public  string DebitAccountId { get; private set; }
    public  Money Amount { get; private set; }

    public TransactionAccountDetail SetAmount(decimal amount)
    {
        StopIfAmountBeLessThanZeroOrEquals(amount);
        Amount = Amount;
        return this;
    }

    private static void StopIfAmountBeLessThanZeroOrEquals(decimal amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Transaction Amount Value Can Not Be Less Than Zero Or Equals");
    }

    public TransactionAccountDetail SetDebitAccountId(string debitAccountId)
    {
        DebitAccountId = debitAccountId;
        return this;
    }

    public TransactionAccountDetail SetCreditAccountId(string creditAccountId)
    {
        CreditAccountId = creditAccountId;
        return this;
    }
}
