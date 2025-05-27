using StockSimulator.Data.Models.Projection;

public class StockProfitAndLossSummaryResult
{
    public List<StockProfitAndLossData> Items { get; set; } = [];
    public decimal GrandTotalGrossProfit { get; set; }
    public decimal GrandTotalDividends { get; set; }
    public decimal GrandTotalFees { get; set; }
}
