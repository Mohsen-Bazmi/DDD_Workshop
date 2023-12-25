namespace InternalMessaging;

public interface IRetry
{
    void Execute(Action callback);
}
