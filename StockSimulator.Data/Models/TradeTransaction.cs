namespace StockSimulator.Data.Models;

public class TradeTransaction
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
    public string ReferenceId { get; set; }
    public DateTime ImportDate { get; set; }
    public Stock Stock { get; set; }
    public virtual Agent? Agent { get; set; }
    public virtual Buyer? Buyer { get; set; }
    public virtual ProfitAndLoss? ProfitAndLoss { get; set; }
}
