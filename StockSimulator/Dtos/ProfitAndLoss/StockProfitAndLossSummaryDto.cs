using StockSimulator.Data.Models;

namespace StockSimulator.Dtos.ProfitAndLoss;

public class StockProfitAndLossSummaryDto
{
    public List<StockProfitAndLossDataDto>? Items { get; set; }
    public decimal GrandTotalGrossProfit { get; set; }
    public decimal GrandTotalDividends { get; set; }
    public decimal GrandTotalFees { get; set; }
}