using AutoFixture.Xunit2;
using FluentAssertions;
namespace Services.Spec;

public class TransactionOrchestratorSpecs
{
    [Theory, AutoMoqDataAttributeWithPositiveDecimals]
    public void Transfer_adds_the_balance_to_the_debit_account(
        string debitAccountId,
        string creditAccountId,
        [Frozen(Matching.ImplementedInterfaces)] InMemoryAccounts __,
        [Frozen(Matching.ImplementedInterfaces)] InMemoryTransactions ___,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountOrchestrator,
        AccountQueries queries,
        string transactionId,
        decimal amount
    )
    {
        amount = Math.Abs(amount);

        accountOrchestrator.OpenAccount(creditAccountId, amount + 20000);

        sut.DraftTransfer(transactionId,
            creditAccountId, debitAccountId,
            amount);

        sut.CommitTransfer(transactionId);

        queries.GetBalanceForAccount(debitAccountId).Should()
            .BeEquivalentTo(new { Balance = amount });
    }


    [Theory, AutoMoqDataAttributeWithPositiveDecimals]
    public void Transfer_subtracts_the_balance_from_the_credit_account(
       [Frozen(Matching.ImplementedInterfaces)] InMemoryAccounts __,
        [Frozen(Matching.ImplementedInterfaces)] InMemoryTransactions ___,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountService,
        AccountQueries queries,
        string transactionId,
        decimal amount,
        string debitAccountId
        )
    {
        amount = Math.Abs(amount);
        var creditAccount = Build.AnAccount.WithBalance(amount + 25000).Please();

        accountService.OpenAccount(creditAccount.Id.Id, creditAccount.Balance.Value);

        sut.DraftTransfer(transactionId,
            creditAccount.Id.Id, debitAccountId,
            amount);

        sut.CommitTransfer(transactionId);

        queries.GetBalanceForAccount(creditAccount.Id.Id).Should()
            .BeEquivalentTo(new { Balance = 25000 });
    }

    [Theory, AutoMoqDataAttributeWithPositiveDecimals]
    public void Drafts_a_new_transaction(
        [Frozen(Matching.ImplementedInterfaces)] InMemoryAccounts __,
        [Frozen(Matching.ImplementedInterfaces)] InMemoryTransactions ___,
        TransactionOrchestrator sut,
        TransactionQueries queries,
        DateTime now,
        string creditAccountId,
        string debitAccountId,
        decimal amount
    )
    {
        amount = Math.Abs(amount);

        sut.DraftTransfer("transaction Id", creditAccountId, debitAccountId, amount);

        queries.AllDrafts().Should().Contain(new TransferDraftViewModel(
            creditAccountId,
            debitAccountId,
            amount
        ));

    }

}