namespace StockSimulator.Dtos.TradeTransaction;

public class MatchedStockRequestDto
{
    public int StartingProfitAndLossId { get; set; }
    public int BuyerId { get; set; }
    public List<int> StockIds { get; set; } = new();
}