namespace FunctionalLibrary;

public class Either<L, R> where R : class where L : class
{
    readonly L? left;
    readonly R? right;

    public L GetLeft() => left ?? throw new InvalidOperationException();
    public R GetRight() => right ?? throw new InvalidOperationException();

    protected Either(L? left, R? right)
    {
        this.left = left;
        this.right = right;
    }
    public T Match<T>(Func<L, T> left, Func<R, T> right)
    {
        if (IsLeft) return left(GetLeft());
        return right(GetRight());

    }

    public static Either<L, R> Left(L value) => new Either<L, R>(value, null);

    public static Either<L, R> Right(R value) => new Either<L, R>(null, value);

    public static implicit operator Either<L, R>((L? left, R? right) x)
    {
        return new Either<L, R>(x.left, x.right);
    }

    public bool IsLeft => left is not null;
    public bool IsRight => !IsLeft;


}

public static class EitherExtensions
{
    public static R? Select<L, R>(this Either<L, R> either, Action<L> left) where R : class where L : class
        => either.Match<R?>(
                l =>
                {
                    left(l);
                    return null;
                },
                r => r
            );
}

