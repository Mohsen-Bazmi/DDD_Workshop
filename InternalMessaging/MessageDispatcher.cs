using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;

namespace InternalMessaging;

public class MessageDispatcher : IMessageDispatcher
{
    readonly IEnumerable<Type> allHandlers;
    readonly string handlerMethodName;
    readonly IRetry retry;
    public MessageDispatcher(IServiceProvider serviceProvider, Type messageHandlerType, string handlerMethodName, Assembly messageHandlerAssembly, IRetry retry)
    {
        if (!messageHandlerType.IsGenericType) throw new InvalidOperationException($"{nameof(messageHandlerType)} must be generic");
        if (!messageHandlerType.GetMethods().Any(m => m.Name == handlerMethodName))
            throw new InvalidOperationException($"The message handler type requires at leat one method named `{handlerMethodName}`");

        this.handlerMethodName = handlerMethodName;
        this.serviceProvider = serviceProvider;
        allHandlers = messageHandlerAssembly.GetHandlerTypes(messageHandlerType);

        this.retry = retry;
    }
    readonly IServiceProvider serviceProvider;

    public void Publish(IEnumerable messages)
    {
        foreach (var handlerType in allHandlers)
            foreach (var message in messages)
            {
                if (handlerType.GetInterfaces().Any(i => message.GetType().IsAssignableTo(i.GetGenericArguments().First())))
                {
                    //TODO: Optimize this:
                    var methods = handlerType.GetMethods().Where(m => m.Name == handlerMethodName && m.GetParameters().Length == 1 && m.GetParameters().First().ParameterType.IsAssignableFrom(message.GetType()));
                    foreach (var method in methods)
                    {

                        try
                        {
                            retry.Execute(() =>
                             {
                                 var instance = Instanciate(handlerType);
                                 method.Invoke(instance, new[] { message });
                             });
                        }
                        catch (Exception ex)
                        {
                            //Log it
                            Console.WriteLine($"Failed to accomplish handling the event: {message.GetType().Name}. \n Exception: {ex.Message}");
                        }
                    }
                }
            }
    }

    object Instanciate(Type handlerType)
    => ActivatorUtilities.CreateInstance(serviceProvider, handlerType);
}
