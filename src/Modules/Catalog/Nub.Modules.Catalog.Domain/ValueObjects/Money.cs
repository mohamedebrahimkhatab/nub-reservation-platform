using Nub.Shared.Kernel;

namespace Nub.Modules.Catalog.Domain.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

#pragma warning disable CS8618
    private Money() { }
#pragma warning restore CS8618

    public Money(decimal amount, string currency = "EGP")
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required", nameof(currency));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required.", nameof(currency));

        Amount = amount;
        Currency = currency;
    }

    public Money Add(Money money)
    {
        if (Currency != money.Currency)
            throw new InvalidOperationException("Cannot add money with different currencies.");

        return new Money(Amount + money.Amount, Currency);
    }

    public Money Subtract(Money money)
    {
        if (Currency != money.Currency)
            throw new InvalidOperationException("Cannot subtract money with different currencies.");

        if (Amount < money.Amount)
            throw new InvalidOperationException("Resulting amount cannot be negative.");


        return new Money(Amount - money.Amount, Currency);
    }

    public Money Multiply(decimal multiplier)
    {
        return new Money(Amount * multiplier, Currency);
    }

    public static Money Zero(string currency = "EGP")
    {
        return new Money(0, currency);
    }

    public bool IsZero()
    {
        return Amount == 0;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    override public string ToString()
    {
        return $"{Amount} {Currency}";
    }
}
