using AutoFixture.Xunit2;
using FluentAssertions;

public class AccountOrchestratorSpec
{
    [Theory, AutoMoqDataAttributeWithPositiveDecimals]
    public void Opens_a_new_account(string accountId, decimal balance,
        [Frozen(Matching.ImplementedInterfaces)] InMemoryAccounts __,
        AccountQueries queries,
        AccountOrchestrator accountOrchestrator
    )
    {
        accountOrchestrator.OpenAccount(new OpenAccountCommand(accountId, Math.Abs(balance)));
        queries.GetBalanceForAccount(accountId).Should().BeEquivalentTo(new { Balance = Math.Abs(balance) });
    }
}