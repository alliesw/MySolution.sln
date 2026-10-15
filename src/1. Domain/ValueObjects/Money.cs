namespace Domain.ValueObjects;

public record Money(decimal Amount, string Currency)
{
  public static Money Zero(string currency = "USD") => new(0m, currency);
  public Money Add(Money other)
  {
    if(Currency != other.Currency) throw new invalidOperationException("Currency mismatch");
    return new Money(Amount + other.Amount, Currency);
  }
}
  
    
