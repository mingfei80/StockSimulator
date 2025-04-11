using StockSimulator.Data.Models;

namespace StockSimulator.Dtos.ProfitAndLoss;

public class TradeFeeDto
{
    public int Id { get; set; }
    public required string FeeType { get; set; }
    public decimal Amount { get; set; }
    public required string ReferenceId { get; set; }
    public DateTime TradeDate { get; set; }
    public DateTime SettleDate { get; set; }
    public DateTime ImportDate { get; set; }
}