using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public class Account
    {
        public int Id { get; }
        public Money Balance { get; protected set; }

        public Account(Money initialBalance, int id)
        {
            Balance = initialBalance;
            Id = id;
        }

        public void Deposit(Money amount)
        {
            Balance = Balance.Add(amount);
        }


    }
}
