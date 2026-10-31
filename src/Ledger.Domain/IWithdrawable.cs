using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public interface IWithdrawable
    {
        void Withdraw(Money amount);
    }
}
