using FluentAssertions;
namespace Services.Spec;

public class TransactionOrchestratorSpecs:IDisposable
{
    [Fact]
    public void Transfer_adds_the_balance_to_the_debit_account()
    {
        var creaditAccount = Build.AnAccount.WithBalance(20000).Please();

        accounts.Add(creaditAccount);
        var queries = new TransactionQueries();
        var orchestrator = new TransactionOrchestrator(accounts);
        orchestrator.Transfer(creaditAccount.Id, "456", 10000);
        queries.GetBalanceForAccount("456")
        .Should().BeEquivalentTo(new BalanceViewModel(
            Id: "456",
            Balance: 10000
        ));
    }

    
    [Fact]
    public void Transfer_subtracts_the_balance_to_the_debit_account()
    {
        var creaditAccount = Build.AnAccount.WithBalance(25000).Please();

        accounts.Add(creaditAccount);
        var queries = new TransactionQueries();
        var orchestrator = new TransactionOrchestrator(accounts);
        orchestrator.Transfer(creaditAccount.Id, "456", 10000);
        queries.GetBalanceForAccount(creaditAccount.Id)
        .Should().BeEquivalentTo(new BalanceViewModel(
            Id: creaditAccount.Id,
            Balance: 15000
        ));
    }

    public void Dispose() => accounts.Clear();
    Accounts accounts = new Accounts();
}