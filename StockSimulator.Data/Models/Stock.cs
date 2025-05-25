namespace StockSimulator.Data.Models;
public class Stock
{
    public int Id { get; set; }

    public required string StockCode { get; set; }

    public required string StockName { get; set; }

    public virtual ICollection<TradeTransaction>? TradeTransactions { get; set; }
    public virtual ICollection<Dividend>? Dividends { get; set; }
    public virtual ICollection<TradeFee>? TradeFees { get; set; }
    public virtual ICollection<SnapshotStockPrice>? SnapshotStockPrices { get; set; }
}