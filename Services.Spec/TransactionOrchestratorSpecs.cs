using AutoFixture.Xunit2;
using FluentAssertions;
namespace Services.Spec;

public class TransactionOrchestratorSpecs 
{
    [Theory, AutoMoqData]
    public void Transfer_adds_the_balance_to_the_debit_account(
        string debitAccountId,
        [Frozen] Accounts accounts,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        TransactionOrchestrator orchestrator,
        TransactionQueries queries
    )
    {
        var creditAccount = Build.AnAccount.WithBalance(20000).Please();
        
        accounts.Add(creditAccount);
        
        orchestrator.Transfer(creditAccount.Id, debitAccountId, 10000);
        
        queries.GetBalanceForAccount(debitAccountId).Should().BeEquivalentTo(new { Balance = 10000 });
    }


    [Theory, AutoMoqData]
    public void Transfer_subtracts_the_balance_to_the_credit_account(
        [Frozen] Accounts accounts,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        TransactionOrchestrator orchestrator,
        TransactionQueries queries)
    {
        var creditAccount = Build.AnAccount.WithBalance(25000).Please();

        accounts.Add(creditAccount);

        orchestrator.Transfer(creditAccount.Id, "dummy", 10000);

        queries.GetBalanceForAccount(creditAccount.Id).Should().BeEquivalentTo(new { Balance = 15000 });
    }

}