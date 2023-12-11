using System.Reflection;
using InternalMessaging;
using Services;

namespace Presentation;

public static class BusinessServicesRegisterer
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IMessageDispatcher, MessageDispatcher>(s => new MessageDispatcher(s, typeof(IHandleMessage<>), Assembly.Load("Services")));
        services.AddSingleton<Accounts, InMemoryAccounts>();
        services.AddSingleton<Transactions, InMemoryTransactions>();
    }

    public static void RegisterHandlers(this IServiceCollection services)
    {
        // services.AddTransient<IHandleMessage<TransactionCommited>, EventAuditor>();
        services.AddTransient<TransactionOrchestrator>();
        services.AddTransient<AccountOrchestrator>();
        services.AddTransient<DateTimeService, DateTimeServiceImplementation>();
    }

    public static void RegisterDomainServices(this IServiceCollection services)
    {
        services.AddTransient<ITransferService, TransferService>();
    }
}