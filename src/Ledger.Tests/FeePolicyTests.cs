using System;
using System.Collections.Generic;
using System.Text;
using Ledger.Domain;

namespace Ledger.Tests
{
    public class FeePolicyTests
    {

        [Fact]
        public void FlatFeeCalculator_CorrectAmountAndCurrency_ReturnsFeeAmount()
        {
            var test = new FlatFeePolicy();

            var a = test.Calculate(new Money(1000, "TRY"));

            Assert.Equal(new Money(5, "TRY"), a);
        }

        [Fact]
        public void PercentageFeeCalculator_CorrectAmountAndCurrency_ReturnsFeeAmount()
        {
            var test = new PercentageFeePolicy();

            var a = test.Calculate(new Money(1000, "TRY"));

            Assert.Equal(new Money(10, "TRY"), a);
        }

        [Fact]
        public void TieredFeeCalculator_CorrectAmountAndCurrency_ReturnsFeeAmount()
        {
            var test = new TieredFeePolicy();

            var a = test.Calculate(new Money(1000, "TRY"));
            var b = test.Calculate(new Money(50, "TRY"));

            Assert.Equal(new Money(10, "TRY"), a);
            Assert.Equal(new Money(5, "TRY"), b);
        }

    }

    public class TieredFeePolicy() : IFeePolicy
    {
        public Money Calculate(Money amount)
        {
            if (amount.Amount < 100) return new Money(5, amount.Currency);
            else return new Money(10, amount.Currency);
        }
    }
}

