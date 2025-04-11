using StockSimulator.Data.Models;

namespace StockSimulator.Dtos.ProfitAndLoss;

public class DividendDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime TradeDate { get; set; }
    public DateTime SettleDate { get; set; }
    public DateTime ImportDate { get; set; }
}