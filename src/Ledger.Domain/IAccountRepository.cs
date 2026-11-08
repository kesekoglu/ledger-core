using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public interface IAccountRepository
    {
        void AddAccount(Account account);

        Account GetById(int id);
    }
}
