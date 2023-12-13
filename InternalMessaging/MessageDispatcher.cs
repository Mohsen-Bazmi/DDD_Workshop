using System.Collections;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace InternalMessaging;

public class MessageDispatcher : IMessageDispatcher
{
    readonly IEnumerable<Type> allHandlers;
    public MessageDispatcher(IServiceProvider serviceProvider, Type messageHandlerType, Assembly messageHandlerAssembly)
    {
        if (!messageHandlerType.IsGenericType) throw new InvalidOperationException($"{nameof(messageHandlerType)} must be generic");

        this.serviceProvider = serviceProvider;
        allHandlers = GetHandlerTypes(messageHandlerAssembly, messageHandlerType);
    }
    readonly IServiceProvider serviceProvider;

    public void Dispatch(IEnumerable messages)
    {
        foreach (var handlerType in allHandlers)
            foreach (var message in messages)
            {
                if (handlerType.GetInterfaces().Any(i => message.GetType().IsAssignableTo(i.GetGenericArguments().First())))
                {
                    var methods = handlerType.GetMethods().Where(m => /* m.name == hanlderType.Method.name*/ m.GetParameters().Length == 1 && m.GetParameters().First().ParameterType.IsAssignableFrom(message.GetType()));
                    foreach (var method in methods)
                    {
                        var instance = Instanciate(handlerType);
                        method.Invoke(instance, new[] { message });

                    }
                }
            }
    }

    object Instanciate(Type handlerType)
    => ActivatorUtilities.CreateInstance(serviceProvider, handlerType);


    static IEnumerable<Type> GetHandlerTypes(Assembly messageHandlerAssembly, Type messageHandlerType)
    => messageHandlerAssembly.GetTypes()
        .SelectMany(type =>
        {
            if (type.GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition().IsAssignableTo(messageHandlerType)))
                return new[] { type };

            return new Type[0];
        });


}