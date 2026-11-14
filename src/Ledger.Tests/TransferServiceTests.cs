using Ledger.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Tests
{
    public class TransferServiceTests
    {
        [Fact]
        public void Transfer_SufficientBalance_MovesMoney()
        {
            var money = new Money(100m, "TRY");
            var transferAmount = new Money(30m, "TRY");
            var from = new CurrentAccount(money, 1);
            var to = new CurrentAccount(money, 2);

            var repo = new InMemoryAccountRepository();
            repo.AddAccount(from);
            repo.AddAccount(to);

            var service = new TransferService(repo);

            service.Transfer(from.Id, to.Id, transferAmount);

            Assert.Equal(130m, to.Balance.Amount);
            Assert.Equal(70m, from.Balance.Amount);
        }

        [Fact]
        public void Transfer_FromNonWithdrawableAccount_Throws()
        {
            var money = new Money(100m, "TRY");
            var transferAmount = new Money(30m, "TRY");
            var from = new Account(money, 1);
            var to = new CurrentAccount(money, 2);

            var repo = new InMemoryAccountRepository();
            repo.AddAccount(from);
            repo.AddAccount(to);

            var service = new TransferService(repo);

            var ex = Assert.Throws<ArgumentException>(() => service.Transfer(from.Id, to.Id, transferAmount));

            Assert.Equal(100m, to.Balance.Amount);
            Assert.Equal("Can't withdraw from this account", ex.Message);
        }

    }
}
