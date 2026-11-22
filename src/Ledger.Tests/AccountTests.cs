using Ledger.Domain;
using Xunit;

namespace Ledger.Tests;

public class AccountTests
{
    [Fact]
    public void Deposit_SameCurrency_IncreasesBalance()
    {
        var initialBalance = new Money(100m, "TRY");
        var depositAmount = new Money(50m, "TRY");
        var expectedBalance = new Money(150m, "TRY");

        var account = new CurrentAccount(initialBalance, 1);

        account.Deposit(depositAmount);

        Assert.Equal(expectedBalance, account.Balance);
    }

    [Fact]
    public void Withdraw_SufficientBalance_DecreasesBalance()
    {
        var initialBalance = new Money(100m, "TRY");
        var withdrawAmount = new Money(30m, "TRY");
        var expectedBalance = new Money(70m, "TRY");

        var account = new CurrentAccount(initialBalance, 1);

        account.Withdraw(withdrawAmount);

        Assert.Equal(expectedBalance, account.Balance);
    }

    [Fact]
    public void Withdraw_InsufficientBalance_ThrowsArgumentException()
    {
        var initialBalance = new Money(50m, "TRY");
        var withdrawAmount = new Money(100m, "TRY");

        var account = new CurrentAccount(initialBalance, 1);

        var ex = Assert.Throws<ArgumentException>(() => account.Withdraw(withdrawAmount));

        Assert.Equal("Don't have enough money", ex.Message);
    }

    [Fact]
    public void Deposit_DifferentCurrency_ThrowsCurrencyMismatchException()
    {
        var initialBalance = new Money(100m, "TRY");
        var depositAmount = new Money(50m, "USD");

        var account = new CurrentAccount(initialBalance, 1);

        var ex = Assert.Throws<CurrencyMismatchException>(() => account.Deposit(depositAmount));

        Assert.Equal("Cannot add different currencies", ex.Message);
    }

    [Fact]
    public void Withdraw_ExactBalance_LeavesZeroBalance()
    {
        var initialBalance = new Money(100m, "TRY");
        var withdrawAmount = new Money(100m, "TRY");
        var expectedBalance = new Money(0m, "TRY");

        var account = new CurrentAccount(initialBalance, 1);

        account.Withdraw(withdrawAmount);

        Assert.Equal(expectedBalance, account.Balance);
    }

    [Fact]
    public void Withdraw_AfterMaturity_DecreasesBalance()
    {
        var initialBalance = new Money(100m, "TRY");
        var withdrawAmount = new Money(30m, "TRY");
        var expectedBalance = new Money(70m, "TRY");

        var maturityDate = new DateTime(2025, 1, 1);
        var today = new DateTime(2026, 1, 1);

        var todayClock = new FakeClock(today);

        var account = new FixedDepositAccount(initialBalance, maturityDate, todayClock, 1);

        account.Withdraw(withdrawAmount);

        Assert.Equal(expectedBalance, account.Balance);
    }

    [Fact]
    public void Withdraw_BeforeMaturity_ThrowsArgumentException()
    {
        var initialBalance = new Money(100m, "TRY");
        var withdrawAmount = new Money(30m, "TRY");

        var maturityDate = new DateTime(2026, 1, 1);
        var today = new DateTime(2025, 1, 1);

        var todayClock = new FakeClock(today);

        var account = new FixedDepositAccount(initialBalance, maturityDate, todayClock, 1);

        var ex = Assert.Throws<ArgumentException>(() => account.Withdraw(withdrawAmount));

        Assert.Equal("Can't draw money before Maturity Date", ex.Message);

    }

    [Fact]
    public void Withdraw_OnMaturityDate_DecreasesBalance()
    {
        var initialBalance = new Money(100m, "TRY");
        var withdrawAmount = new Money(30m, "TRY");
        var expectedBalance = new Money(70m, "TRY");

        var maturityDate = new DateTime(2026, 1, 1);
        var today = new DateTime(2026, 1, 1);

        var todayClock = new FakeClock(today);

        var account = new FixedDepositAccount(initialBalance, maturityDate, todayClock, 1);

        account.Withdraw(withdrawAmount);

        Assert.Equal(expectedBalance, account.Balance);
    }

    [Fact]
    public void Equals_SameIdDifferentBalance_ReturnsTrue()
    {
        var before = new FakeAccount(new Money(100m, "TRY"), 1);
        var after = new FakeAccount(new Money(500m, "TRY"), 1);

        Assert.True(before.Equals(after));
    }

    [Fact]
    public void Equals_DifferentIdSameBalance_ReturnsFalse()
    {
        var first = new FakeAccount(new Money(100m, "TRY"), 1);
        var second = new FakeAccount(new Money(100m, "TRY"), 2);

        Assert.False(first.Equals(second));
    }

    [Fact]
    public void HashSet_AfterDeposit_StillContainsAccount()
    {
        var account = new CurrentAccount(new Money(100m, "TRY"), 1);
        var set = new HashSet<Account> { account };

        account.Deposit(new Money(50m, "TRY"));

        Assert.Equal(150m, account.Balance.Amount);
        Assert.True(set.Contains(account));
    }

}