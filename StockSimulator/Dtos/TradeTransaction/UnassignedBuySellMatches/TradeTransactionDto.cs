namespace StockSimulator.Dtos.TradeTransaction.UnassignedBuySellMatches;
public class TradeTransactionDto
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public DateTime TradeDate { get; set; }
    public string? StockName { get; set; }
    public decimal UnitCost { get; set; }
    public decimal Quantity { get; set; }
    public decimal TransactionAmount { get; set; }
    public decimal? ConversionRate { get; set; }
    public decimal? UnitCostForeign { get; set; }
    public int? ProfitAndLossId { get; set; }
    public bool IsSold { get; set; }
}