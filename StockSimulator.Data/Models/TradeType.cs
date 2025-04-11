namespace StockSimulator.Data.Models;

public class TradeType
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public virtual ICollection<TradeFee>? TradeFees { get; set; }
}
