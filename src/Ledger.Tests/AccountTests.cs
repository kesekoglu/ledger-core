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

        var a1 = new Account(m1);

        a1.Deposit(m2);

        Assert.Equal(m3, a1.Balance);
    }

    [Fact]
    public void Withdraw_SufficentBalance_DecreasesBalance()
    {
        var m1 = new Money(100m, "TRY");
        var m2 = new Money(30m, "TRY");
        var m3 = new Money(70m, "TRY");

        var a1 = new Account(m1);

        a1.Withdraw(m2);

        Assert.Equal(m3, a1.Balance);
    }

    [Fact]
    public void Withdraw_InsufficentBalance_ThrowsArgumentException()
    {
        var m1 = new Money(50m, "TRY");
        var m2 = new Money(100m, "TRY");

        var a1 = new Account(m1);

        var ex = Assert.Throws<ArgumentException>(() => a1.Withdraw(m2));

        Assert.Equal("Don't have enough money", ex.Message);
    }

    [Fact]
    public void WithdrawFromFixedDepositAccount_SufficentBalance_DecreasesBalance()
    {
        var m1 = new Money(100m, "TRY");
        var m2 = new Money(30m, "TRY");
        var m3 = new Money(70m, "TRY");

        Account a1 = new FixedDepositAccount(m1);

        a1.Withdraw(m2);

        Assert.Equal(m3, a1.Balance);
    }

    [Fact]
    public void Deposit_DifferentCurrency_ThrowsCurrencyMismatchException()
    {
        var m1 = new Money(100m, "TRY");
        var m2 = new Money(50m, "USD");

        var a1 = new Account(m1);

        var ex = Assert.Throws<CurrencyMismatchException>(() => a1.Deposit(m2));

        Assert.Equal("Cannot add different currencies", ex.Message);
    }

    [Fact]
    public void Withdraw_ExactBalance_LeavesZeroBalance()
    {
        var m1 = new Money(100m, "TRY");
        var m2 = new Money(100m, "TRY");
        var m3 = new Money(0m, "TRY");

        var a1 = new Account(m1);

        a1.Withdraw(m2);

        Assert.Equal(m3, a1.Balance);
    }

}