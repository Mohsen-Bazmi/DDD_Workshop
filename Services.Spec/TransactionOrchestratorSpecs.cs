using AutoFixture.Xunit2;
using FluentAssertions;
namespace Services.Spec;

public class TransactionOrchestratorSpecs
{
    [Theory, AutoMoqData]
    public void Transfer_adds_the_balance_to_the_debit_account(
        string debitAccountId,
        string creditAccountId,
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountOrchestrator,
        AccountQueries queries,
        string transactionId,
        decimal amount,
        DateTime now,
        string description
    )
    {
        amount = Math.Abs(amount);

        accountOrchestrator.OpenAccount(creditAccountId, amount + 20000);

        sut.DraftTransfer(transactionId,
            creditAccountId, debitAccountId,
            amount, now, description);

        sut.CommitTransfer(transactionId);

        queries.GetBalanceForAccount(debitAccountId).Should()
            .BeEquivalentTo(new { Balance = amount });
    }


    [Theory, AutoMoqData]
    public void Transfer_subtracts_the_balance_from_the_credit_account(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountService,
        AccountQueries queries,
        string transactionId,
        decimal amount,
        DateTime now,
        string description,
        string debitAccountId
        )
    {
        amount = Math.Abs(amount);
        var creditAccount = Build.AnAccount.WithBalance(amount + 25000).Please();

        accountService.OpenAccount(creditAccount.Id, creditAccount.Balance);

        sut.DraftTransfer(transactionId,
            creditAccount.Id, debitAccountId,
            amount, now, description);

        sut.CommitTransfer(transactionId);

        queries.GetBalanceForAccount(creditAccount.Id).Should()
            .BeEquivalentTo(new { Balance = 25000 });
    }

    [Theory, AutoMoqData]
    public void Drafts_a_new_transaction(
        [Frozen] Transactions _,
        TransactionOrchestrator sut,
        TransactionQueries queries,
        DateTime now,
        string description,
        string creditAccountId,
        string debitAccountId,
        decimal amount
    )
    {
        amount = Math.Abs(amount);

        sut.DraftTransfer("transaction Id", creditAccountId, debitAccountId, amount, now, description);

        queries.AllDrafts().Should().Contain(new TransferDraftViewModel(
            creditAccountId,
            debitAccountId,
            amount,
            now
        ));

    }

}