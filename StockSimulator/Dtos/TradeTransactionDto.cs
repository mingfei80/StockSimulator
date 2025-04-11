
namespace StockSimulator.Dtos;
public class TradeTransactionDto
{
    public int Id { get; set; }
    public DateTime TradeDate { get; set; }
    public DateTime SettleDate { get; set; }
    public decimal UnitCost { get; set; }
    public decimal Quantity { get; set; }
    public decimal TransactionAmount { get; set; }
    public int StockId { get; set; }
    public decimal? ConversionRate { get; set; }
    public int ImportId { get; set; }
    public int AgentId { get; set; }
    public int BuyerId { get; set; }
    public decimal? UnitCostForeign { get; set; }
    public bool IsSold { get; set; }
    public int? ProfitAndLossId { get; set; }
    public required string ReferenceId { get; set; }
    public DateTime ImportDate { get; set; }
    //public StockDto? Stock { get; set; }
    //public AgentDto? Agent { get; set; }
    //public BuyerDto? Buyer { get; set; }
    //public ProfitAndLossDto? ProfitAndLoss { get; set; }
}