namespace StockSimulator.Dtos.ProfitAndLoss;

public class StockIdAndTradeTransactionIds
{
    public int StockId { get; set; }
    public required List<int> TradeTransactionIds { get; set; }
}
