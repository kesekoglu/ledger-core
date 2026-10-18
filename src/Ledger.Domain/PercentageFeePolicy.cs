using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public class PercentageFeePolicy : IFeePolicy
    {
        public Money Calculate(Money amount)
         {
            return new Money(amount.Amount * 0.01m, amount.Currency);
         }
}
}
