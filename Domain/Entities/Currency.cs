using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entites;
public class Currency
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }

    // Child Object
    [NotMapped]
    public List<CurrencyExchange> CurrencyExchanges { get; set; }
}