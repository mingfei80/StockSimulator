namespace StockSimulator.Dtos.ProfitAndLoss;

public class ProfitAndLossDto
{
    public int Id { get; set; }
    public required string StockName { get; set; }
    public decimal GrossProfit { get; set; }
    public decimal TotalFees { get; set; }
    public decimal TotalDividends { get; set; }
    public decimal NetProfit
    {
        get { return GrossProfit - TotalFees + TotalDividends; }
    }
    public int DaysHolding { get; set; }
    public virtual ICollection<TradeTransactionDto>? TradeTransactions { get; set; }
    public virtual ICollection<DividendDto>? Dividends { get; set; }
    public virtual ICollection<TradeFeeDto>? TradeFees { get; set; }
}
