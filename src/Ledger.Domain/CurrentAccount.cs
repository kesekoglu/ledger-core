namespace Ledger.Domain
{
    public class CurrentAccount : Account, IWithdrawable
    {
        public CurrentAccount(Money initialBalance, int id) : base(initialBalance, id) { }

        public void Withdraw(Money amount)
        {
            if (Balance.Amount < amount.Amount) throw new ArgumentException("Don't have enough money");
            Balance = Balance.Subtract(amount);
        }
    }
}
