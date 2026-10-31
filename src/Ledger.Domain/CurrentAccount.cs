using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Ledger.Domain
{
    public class CurrentAccount : Account, IWithdrawable
    {
        public CurrentAccount(Money initialBalance) : base(initialBalance) { }

        public void Withdraw(Money amount)
        {
            if (Balance.Amount < amount.Amount) throw new ArgumentException("Don't have enough money");
            Balance = Balance.Subtract(amount);
        }
    }
}
