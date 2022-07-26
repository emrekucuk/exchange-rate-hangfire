namespace Domain.Entites;
public class CurrencyExchangeDto
{
    public Guid Id { get; set; }
    public string Value { get; set; }
    public DateTime Date { get; set; }
    public string CurrencyName { get; set; }
}