using System.Text.Json.Serialization;

namespace ContextLab.Entities.Structs
{
    public readonly struct AnnualFinance
    {
        [JsonConstructor]
        public AnnualFinance(int year, Money income, Money expenses)
        {
            Year = year;
            Income = income;
            Expenses = expenses;
        }
        public int Year { get; }
        public Money Income { get; }
        public Money Expenses { get; }
        public Money Revenue => new Money(Income.Amount - Expenses.Amount, Income.Currency);
    }


    public readonly struct Money
    {
        [JsonConstructor]
        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
        public override string ToString()
            => (Currency == Currency.UsDollars ? "$" : "£") + Amount;
        public decimal Amount { get; }
        public Currency Currency { get; }
    }

    public enum Currency
    {
        UsDollars,
        PoundsStirling
    }
}
