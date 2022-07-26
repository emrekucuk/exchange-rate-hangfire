namespace Domain.Entites;
public class CurrencyExchangeDto
{
    public Guid Id { get; set; }
    public double Value { get; set; }
    public DateTime Date { get; set; }
    public string CurrencyName { get; set; }
}