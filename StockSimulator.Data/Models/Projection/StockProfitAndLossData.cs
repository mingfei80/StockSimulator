namespace StockSimulator.Data.Models.Projection;

public class StockProfitAndLossData
{
    public int StockId { get; set; }
    public required string StockName { get; set; }
    public decimal QuantityHeld { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal GrossProfit { get; set; }
    public decimal TotalDividends { get; set; }
    public decimal TotalFees { get; set; }

    public List<TradeTransaction> BuyTransactions { get; set; } = new();
    public List<TradeTransaction> SellTransactions { get; set; } = new();
    public List<Dividend> Dividends { get; set; } = new();
    public List<TradeFee> Fees { get; set; } = new();
}
