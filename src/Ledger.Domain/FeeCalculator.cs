using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ledger.Domain
{
    public class FeeCalculator
    {
        public Money Calculate(Money amount, string feeType)
        {
            switch (feeType)
            {
                case "flat": return new Money(5,amount.Currency);
                case "percentage": return new Money(amount.Amount * 0.01m, amount.Currency);
                default: throw new ArgumentException("Unknown Fee Type");
            }

        }
    }
}
