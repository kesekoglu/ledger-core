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

            if (from.Balance.Currency != amount.Currency || amount.Currency != to.Balance.Currency)
            {
                throw new CurrencyMismatchException("Cannot add different currencies");
            }

            if (from is IWithdrawable w) w.Withdraw(amount);
            else throw new ArgumentException("Can't withdraw from this account");
            to.Deposit(amount);
        }

        public Result TryTransfer(int fromId, int toId, Money amount)
        {
            Account from;
            Account to;

            try
            {
                from = _repo.GetById(fromId);
                to = _repo.GetById(toId);
            }
            catch (ArgumentException ex)
            {
                return new Result(false, ex.Message);
            }

            if (from.Balance.Currency != amount.Currency || amount.Currency != to.Balance.Currency)
            {
                return new Result(false, "Cannot add different currencies");
            }

            if(from is IWithdrawable w)
            {
                if(from.Balance.Amount < amount.Amount) return new Result(false,"Don't have enough money");
                w.Withdraw(amount);
                to.Deposit(amount);
                return new Result(true, "");
            }
            else
            {
                return new Result(false, "Can't withdraw from this account");
            }
         
        }
    }
}
