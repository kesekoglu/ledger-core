using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public class FlatFeePolicy : IFeePolicy
    {
        public Money Calculate(Money amount)
         {
            return new Money(5, amount.Currency);
         }
}
}
