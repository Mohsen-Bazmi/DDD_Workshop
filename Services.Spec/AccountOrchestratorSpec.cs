using AutoFixture.Xunit2;
using FluentAssertions;

public class AccountOrchestratorSpec
{
    [Theory, AutoMoqData]
    public void Opens_a_new_account(string accountId, decimal balance,
        [Frozen] Accounts _,
        AccountQueries queries,
        AccountOrchestrator accountOrchestrator
    )
    {
        accountOrchestrator.OpenAccount(accountId, Math.Abs(balance));
        queries.GetBalanceForAccount(accountId).Should().BeEquivalentTo(new { Balance = Math.Abs(balance) });
    }
}