using Ledger.Domain;
using Xunit;

namespace Ledger.Tests;

public class AccountTests
{
    [Fact]
    public void Deposit_SameCurrency_IncreasesBalance()
    {
        var m1 = new Money(100m, "TRY");
        var m2 = new Money(50m, "TRY");
        var m3 = new Money(150m, "TRY");

        var a1 = new CurrentAccount(m1, 1);

        a1.Deposit(m2);

        Assert.Equal(m3, a1.Balance);
    }

    [Fact]
    public void Withdraw_SufficentBalance_DecreasesBalance()
    {
        var m1 = new Money(100m, "TRY");
        var m2 = new Money(30m, "TRY");
        var m3 = new Money(70m, "TRY");

        var a1 = new CurrentAccount(m1, 1);

        a1.Withdraw(m2);

        Assert.Equal(m3, a1.Balance);
    }

    [Fact]
    public void Withdraw_InsufficentBalance_ThrowsArgumentException()
    {
        var m1 = new Money(50m, "TRY");
        var m2 = new Money(100m, "TRY");

        var a1 = new CurrentAccount(m1, 1);

        var ex = Assert.Throws<ArgumentException>(() => a1.Withdraw(m2));

        Assert.Equal("Don't have enough money", ex.Message);
    }

    [Fact]
    public void Deposit_DifferentCurrency_ThrowsCurrencyMismatchException()
    {
        var m1 = new Money(100m, "TRY");
        var m2 = new Money(50m, "USD");

        var a1 = new CurrentAccount(m1, 1);

        var ex = Assert.Throws<CurrencyMismatchException>(() => a1.Deposit(m2));

        Assert.Equal("Cannot add different currencies", ex.Message);
    }

    [Fact]
    public void Withdraw_ExactBalance_LeavesZeroBalance()
    {
        var m1 = new Money(100m, "TRY");
        var m2 = new Money(100m, "TRY");
        var m3 = new Money(0m, "TRY");

        var a1 = new CurrentAccount(m1, 1);

        a1.Withdraw(m2);

        Assert.Equal(m3, a1.Balance);
    }

    [Fact]
    public void Withdraw_AfterMaturity_DecreasesBalance()
    {
        var m1 = new Money(100m, "TRY");
        var m2 = new Money(30m, "TRY");
        var m3 = new Money(70m, "TRY");

        var maturityDate = new DateTime(2025, 1, 1);
        var today = new DateTime(2026, 1, 1);

        var todayClock = new FakeClock(today);

        var a1 = new FixedDepositAccount(m1, maturityDate, todayClock, 1);

        a1.Withdraw(m2);

        Assert.Equal(m3, a1.Balance);
    }

    [Fact]
    public void Withdraw_BeforeMaturity_ThrowsArgumentException()
    {
        var m1 = new Money(100m, "TRY");
        var m2 = new Money(30m, "TRY");

        var maturityDate = new DateTime(2026, 1, 1);
        var today = new DateTime(2025, 1, 1);

        var todayClock = new FakeClock(today);

        var a1 = new FixedDepositAccount(m1, maturityDate, todayClock, 1);

        var ex = Assert.Throws<ArgumentException>(() => a1.Withdraw(m2));

        Assert.Equal("Can't draw money before Maturity Date", ex.Message);

    }

    [Fact]
    public void Withdraw_OnMaturityDate_DecreasesBalance()
    {
        var m1 = new Money(100m, "TRY");
        var m2 = new Money(30m, "TRY");
        var m3 = new Money(70m, "TRY");

        var maturityDate = new DateTime(2026, 1, 1);
        var today = new DateTime(2026, 1, 1);

        var todayClock = new FakeClock(today);

        var a1 = new FixedDepositAccount(m1, maturityDate, todayClock, 1);

        a1.Withdraw(m2);

        Assert.Equal(m3, a1.Balance);
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