using System;
using System.Collections.Generic;
using System.Text;
using Ledger.Domain;

namespace Ledger.Tests
{
    public class FakeAccount : Account
    {
        public FakeAccount(Money initialBalance, int id) : base(initialBalance, id) { }
    }
}
