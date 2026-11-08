using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public class FixedDepositAccount : Account, IWithdrawable
    {
        DateTime _maturityDate;
        IClock _clock;


        public FixedDepositAccount(Money initialBalance, DateTime maturityDate, IClock clock, int id) : base(initialBalance, id) 
        {
            _maturityDate = maturityDate;
            _clock = clock;
        }

        public void Withdraw(Money amount)
        {
            if (_maturityDate > _clock.Date())
            {
                throw new ArgumentException("Can't draw money before Maturity Date");
            }
            else if(Balance.Amount < amount.Amount)
            {
                throw new ArgumentException("Don't have enough money to withdraw");
            }
            else
            {
                Balance = Balance.Subtract(amount);
            }
        }

    }
}
