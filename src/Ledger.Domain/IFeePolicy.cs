using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public interface IFeePolicy
    {
        Money Calculate(Money amount);
    }
}
