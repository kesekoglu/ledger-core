using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private List<Account> _accounts = new List<Account>();

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public Account GetById(int id)
        {

            foreach (var account in _accounts)
            {
                if (account.Id == id)
                {
                    return account;
                }
            }
            throw new ArgumentException("Account does not exist");
        }
    }
}
