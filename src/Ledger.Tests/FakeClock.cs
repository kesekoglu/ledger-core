using Ledger.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Tests
{
    public class FakeClock : IClock
    {
        DateTime fakeDate;

        public FakeClock(DateTime initialDate)
        {
            fakeDate = initialDate;
        }

        public DateTime Date()
        {
            return fakeDate;
        }
    }
}
