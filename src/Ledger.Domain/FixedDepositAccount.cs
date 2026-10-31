using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public class FixedDepositAccount : Account
    {

        public FixedDepositAccount(Money initialBalance) : base(initialBalance) { }

        public override void Withdraw(Money amount)
        {
            throw new InvalidOperationException("Date isn't right");
        }
    }
}
