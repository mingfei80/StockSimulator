namespace StockSimulator.Dtos.ProfitAndLoss.Group;

public class GroupProfitAndLossRequest
{
    public List<int> TradeTransactionIds { get; set; } = new();
    public List<int> DividendIds { get; set; } = new();
    public List<int> TradeFeeIds { get; set; } = new();
}
