
namespace StockSimulator.Dtos.ProfitAndLoss;
public class TradeTransactionDto
{
    public int Id { get; set; }
    public DateTime TradeDate { get; set; }
    public DateTime SettleDate { get; set; }
    public decimal UnitCost { get; set; }
    public decimal Quantity { get; set; }
    public decimal TransactionAmount { get; set; }
    public decimal? ConversionRate { get; set; }
    public int ImportId { get; set; }
    public decimal? UnitCostForeign { get; set; }
    public bool IsSold { get; set; }
    public DateTime ImportDate { get; set; }
}