using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public class Account
    {
        public Money Balance { get; protected set; }

        public Account(Money initialBalance)
        {
            Balance = initialBalance;
        }

        public void Deposit(Money amount)
        {
            Balance = Balance.Add(amount);
        }


    }
}
