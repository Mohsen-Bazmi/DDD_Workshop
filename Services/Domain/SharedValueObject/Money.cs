public class Money : IEquatable<Money>
{
    public decimal Value { get; }
    public Money(decimal amount)
    {
        if (amount < 0) throw new InvalidOperationException("Money cannot be negative.");
        Value = amount;
    }

    public static Money operator -(Money left, Money right)
    => new Money(left.Value - right.Value);

    public static Money operator +(Money left, Money right)
    => new Money(left.Value + right.Value);

    public static bool operator <(Money left, Money right)
    => left.Value < right.Value;

    public static bool operator >(Money left, Money right)
    => left.Value > right.Value;

    public static bool operator <=(Money left, Money right)
    => left.Value <= right.Value;

    public static bool operator >=(Money left, Money right)
    => left.Value >= right.Value;

    public static implicit operator Money(decimal amount)
    => new Money(amount);

    public Money Add(Money amountToAdd)
    => new Money(Value + amountToAdd.Value);

    public override bool Equals(object? obj)
    {
        if (!(obj is Money money)) return false;
        return Equals(money);
    }


    public static bool operator ==(Money obj1, Money obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true;
        if (ReferenceEquals(obj1, null))
            return false;
        if (ReferenceEquals(obj2, null))
            return false;
        return obj1.Equals(obj2);
    }
    public static bool operator !=(Money obj1, Money obj2) => !(obj1 == obj2);
    public bool Equals(Money? other)
    {
        if (ReferenceEquals(other, null))
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return Value.Equals(other.Value);
    }


    public override int GetHashCode()
    => Value.GetHashCode();
}