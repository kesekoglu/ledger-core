using Ledger.Domain;
using Xunit;

namespace Ledger.Tests;

public class MoneyTests
{
    [Fact]
    public void Constructor_NegativeAmount_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Money(-10m, "USD"));
    }

    [Fact]
    public void Add_DifferentCurrencies_ThrowsCurrencyMismatchException()
    {
        var m1 = new Money(100m, "TRY");
        var m2 = new Money(50m, "USD");

        Assert.Throws<CurrencyMismatchException>(() => m1.Add(m2));
    }
    [Fact]
    public void Constructor_InvalidCurrency_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Money(100m, "US"));
    }

    [Fact]
    public void Add_ValidAmount_DoesNotMutateOriginal()
    {
        var original = new Money(100m, "TRY");
        var result = original.Add(new Money(50m, "TRY"));

        Assert.Equal(150m, result.Amount);
        Assert.Equal(100m, original.Amount);
    }

    [Fact]
    public void Subtract_ValidAmount_ReturnsCorrectResult()
    {
        var m1 = new Money(100m, "TRY");
        var m2 = new Money(40m, "TRY");

        var result = m1.Subtract(m2);

        Assert.Equal(60m, result.Amount);
    }

    [Fact]
    public void Decimal_IsExact_Whereas_DoubleHasPrecisionLoss()
    {
        double d1 = 0.1;
        double d2 = 0.2;
        Assert.NotEqual(0.3, d1 + d2);

        decimal m1 = 0.1m;
        decimal m2 = 0.2m;
        Assert.Equal(0.3m, m1 + m2);
    }

    [Fact]
    public void Equals_SameAmountAndCurrency_ReturnsTrue()
    {
        var a = new Money(10, "TRY");
        var b = new Money(10, "TRY");
        Assert.Equal(a, b);
    }

    [Fact]
    public void GetHashCode_SameAmountAndCurrency_ShouldHaveOneHashElement()
    {
        var set = new HashSet<Money>();
        set.Add(new Money(10, "TRY"));
        set.Add(new Money(10, "TRY"));
        Assert.Equal(1, set.Count);
    }

    [Fact]
    public void Equals_SameAmountAndDifferentCurrency_ReturnsFalse()
    {
        var a = new Money(10, "TRY");
        var b = new Money(10, "USD");
        Assert.NotEqual(a,b);
    }
}