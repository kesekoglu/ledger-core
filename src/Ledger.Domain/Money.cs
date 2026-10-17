namespace Ledger.Domain
{
    public class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public override bool Equals(object? obj)
        {
            if(obj is Money m && Currency == m.Currency && Amount == m.Amount)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency);
        }

        public Money(decimal amount, string currency)
        {
            if (amount < 0) throw new ArgumentException("Amount cant be negative");
            if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3) throw new ArgumentException("Currency Exception");

            Amount = amount;
            Currency = currency;

        }

        public Money Add(Money other)
        {
            if (Currency != other.Currency) throw new CurrencyMismatchException("Cannot add different currencies");

            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            if (Currency != other.Currency) throw new CurrencyMismatchException("Cannot add different currencies");

            return new Money(Amount - other.Amount, Currency);
        }

    }
}
