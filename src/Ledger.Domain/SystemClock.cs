using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public class SystemClock : IClock
    {
        public DateTime Date()
        {
            return DateTime.Now;
        }
    }
}
