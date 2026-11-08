using Ledger.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Tests
{
    public class AccountRepositoryTests
    {
        [Fact]
        public void GetByID_ExistingId_ReturnsAccount()
        {
            var m1 = new Money(100m, "TRY");
            var a1 = new Account(m1, 1);
            var repo = new InMemoryAccountRepository();
            repo.AddAccount(a1);

            var returnedAccount = repo.GetById(1);

            Assert.Equal(a1, returnedAccount);
        }

        [Fact]
        public void GetById_UnknownId_ThrowsArgumentException()
        {
            var repo = new InMemoryAccountRepository();
 
            var ex = Assert.Throws<ArgumentException>(() => repo.GetById(99));

            Assert.Equal("Account does not exist", ex.Message);
        }
    }
}
