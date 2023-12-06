using FunctionalLibrary;

public class Money : ValueObject
{
    public decimal Value { get; }
    protected Money(decimal amount)
    {
        Value = amount;
    }

    public static Either<Money, NegativeMoneyException> Create(decimal amount)
    {
        if (amount < 0) return Either<Money, NegativeMoneyException>.Right(new NegativeMoneyException());
        return Either<Money, NegativeMoneyException>.Left(new Money(amount));
    }

    public static Either<Money, NegativeMoneyException> operator -(Money left, Money right)
    => Create(left.Value - right.Value);


    public static Either<Money, NegativeMoneyException> operator +(Money left, Money right)
    => Create(left.Value + right.Value);

    public static bool operator <(Money left, Money right)
    => left.Value < right.Value;

    public static bool operator >(Money left, Money right)
    => left.Value > right.Value;

    public static bool operator <=(Money left, Money right)
    => left.Value <= right.Value;

    public static bool operator >=(Money left, Money right)
    => left.Value >= right.Value;

    // public static implicit operator Money(decimal amount)
    // => Create(amount);

    public Either<Money, NegativeMoneyException> Add(Money amountToAdd)
    => Create(Value + amountToAdd.Value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
