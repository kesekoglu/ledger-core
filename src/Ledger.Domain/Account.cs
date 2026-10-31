using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public class Account
    {
        public Money Balance { get; private set; }

        public Account(Money initialBalance)
        {
            Balance = initialBalance;
        }

        public void Deposit(Money amount)
        {
            Balance = Balance.Add(amount);
        }

        public virtual void Withdraw(Money amount)
        {
            if (Balance.Amount < amount.Amount) throw new ArgumentException("Don't have enough money");
            Balance = Balance.Subtract(amount);
        }
    }
}
