using AutoFixture.Xunit2;
using FluentAssertions;
namespace Services.Spec;

public class TransactionOrchestratorSpecs
{
    [Theory, AutoMoqData]
    public void Transfer_adds_the_balance_to_the_debit_account(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountOrchestrator,
        AccountQueries queries,
        string transactionId,
        DateTime now,
        decimal amount,
        string description
    )
    {
        amount = Math.Abs(amount);
        var accountDetail =
            new TransactionAccountDetail(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                amount);

        accountOrchestrator.OpenAccount(
            accountDetail.CreditAccountId,
            accountDetail.Amount.Value + 20000);

        sut.DraftTransfer(
            transactionId,
            accountDetail,
            now,
            description);

        sut.CommitTransfer(transactionId);

        queries.GetBalanceForAccount(accountDetail.DebitAccountId).Should()
            .BeEquivalentTo(new { Balance = accountDetail.Amount.Value });
    }


    [Theory, AutoMoqData]
    public void Transfer_subtracts_the_balance_from_the_credit_account(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountService,
        AccountQueries queries,
        string transactionId,
        DateTime now,
        string description,
        decimal amount)
    {

        amount = Math.Abs(amount);

        var creditAccount = Build.AnAccount.WithBalance(+25000 + amount).Please();
        var accountDetail =
            new TransactionAccountDetail(
                creditAccount.Id,
                Guid.NewGuid().ToString(),
                amount);

        accountService.OpenAccount(creditAccount.Id, creditAccount.Balance.Value);

        sut.DraftTransfer(transactionId,
            accountDetail, now, description);

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
        decimal amount
    )
    {
        amount = Math.Abs(amount);
        var accountDetail =
            new TransactionAccountDetail(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                amount);

        sut.DraftTransfer("transaction Id", accountDetail, now, description);

        queries.AllDrafts().Should().Contain(
            new TransferDraftViewModel(
                accountDetail.CreditAccountId,
                accountDetail.DebitAccountId,
                accountDetail.Amount.Value,
                now
        ));

    }

}