namespace StockSimulator.Data.Models;

public class Dividend
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int ImportId { get; set; }
    public int? ProfitAndLossId { get; set; }
    public required string ReferenceId { get; set; }
    public int StockId { get; set; }
    public int AgentId { get; set; }
    public int BuyerId { get; set; }
    public DateTime TradeDate { get; set; }
    public DateTime SettleDate { get; set; }
    public DateTime ImportDate { get; set; }

    public virtual Stock? Stock { get; set; }
    public virtual Agent? Agent { get; set; }
    public virtual Buyer? Buyer { get; set; }
    public virtual ProfitAndLoss? ProfitAndLoss { get; set; }
}