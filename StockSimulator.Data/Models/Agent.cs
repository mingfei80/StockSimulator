namespace StockSimulator.Data.Models;
public class Agent
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public virtual ICollection<TradeTransaction>? TradeTransactions { get; set; }
    public virtual ICollection<Dividend>? Dividends { get; set; }
    public virtual ICollection<TradeFee>? TradeFees { get; set; }
}

