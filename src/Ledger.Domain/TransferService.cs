using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public class TransferService
    {
        IAccountRepository _repo;

        public TransferService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public void Transfer(int fromId, int toId, Money amount)
        {
            var from = _repo.GetById(fromId);
            var to = _repo.GetById(toId);

            if (from is IWithdrawable w) w.Withdraw(amount);
            else throw new ArgumentException("Can't withdraw from this account");
            to.Deposit(amount);
        }
    }
}
