using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public abstract class Account
    {
        public int Id { get; }
        public Money Balance { get; protected set; }

        public override bool Equals(object? obj)
        {
            if (obj is Account a)
            {
                return Equals(a);
            }
            return false;
        }

        public bool Equals(Account? account)
        {
            if (account is null)
            {
                return false;
            }

            if (Id == account.Id)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

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
