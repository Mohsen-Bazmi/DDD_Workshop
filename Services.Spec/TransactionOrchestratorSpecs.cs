using AutoFixture.Xunit2;
using FluentAssertions;
namespace Services.Spec;

public class TransactionOrchestratorSpecs 
{
    [Theory, AutoMoqData]
    public void Transfer_adds_the_balance_to_the_debit_account(
        string debitAccountId,
        [Frozen] Accounts accounts,
        TransactionOrchestrator orchestrator,
        TransactionQueries queries
    )
    {
        var creaditAccount = Build.AnAccount.WithBalance(20000).Please();
        accounts.Add(creaditAccount);
        orchestrator.Transfer(creaditAccount.Id, debitAccountId, 10000);
        queries.GetBalanceForAccount(debitAccountId)
        .Should().BeEquivalentTo(new BalanceViewModel(
            Id: debitAccountId,
            Balance: 10000
        ));
    }


    [Theory, AutoMoqData]
    public void Transfer_subtracts_the_balance_to_the_debit_account(
        [Frozen] Accounts accounts,
        TransactionOrchestrator orchestrator,
        TransactionQueries queries)
    {
        var creaditAccount = Build.AnAccount.WithBalance(25000).Please();

        accounts.Add(creaditAccount);
        orchestrator.Transfer(creaditAccount.Id, "dummy", 10000);
        queries.GetBalanceForAccount(creaditAccount.Id)
        .Should().BeEquivalentTo(new BalanceViewModel(
            Id: creaditAccount.Id,
            Balance: 15000
        ));
    }

}