using System.Collections;

namespace InternalMessaging;

public interface IMessageDispatcher
{
    void Publish(IEnumerable message);
}
