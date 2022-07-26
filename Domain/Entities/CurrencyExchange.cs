namespace Domain.Entites;
public class CurrencyExchange
{
    public Guid Id { get; set; }
    public double Value { get; set; }
    public DateTime Date { get; set; }

    // Parent Object
    public Guid CurrencyId { get; set; }
    public Currency Currency { get; set; }
}