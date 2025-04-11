namespace StockSimulator.Data.Models;

public class ProfitAndLoss
{
    public int Id { get; set; }
    public decimal GrossProfit { get; set; }
    public decimal TotalFees { get; set; }
    public decimal TotalDividends { get; set; }
    public decimal NetProfit
    {
        get { return GrossProfit - TotalFees + TotalDividends; }
    }
    public int DaysHolding { get; set; }
    public virtual ICollection<TradeTransaction>? TradeTransactions { get; set; }
    public virtual ICollection<Dividend>? Dividends { get; set; }
    public virtual ICollection<TradeFee>? TradeFees { get; set; }
}
