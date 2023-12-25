using System.Reflection;

namespace InternalMessaging;

internal static class Extensions
{
    public static IEnumerable<Type> GetHandlerTypes(this Assembly messageHandlerAssembly, Type messageHandlerType)
        => messageHandlerAssembly.GetTypes()
            .SelectMany(type =>
            {
                if (type.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition().IsAssignableTo(messageHandlerType)))
                    return new[] { type };

                return new Type[0];
            });
}