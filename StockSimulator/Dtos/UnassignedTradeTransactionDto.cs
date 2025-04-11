
namespace StockSimulator.Dtos;
public class UnassignedTradeTransactionDto
{
    public int Id { get; set; }
    public DateTime TradeDate { get; set; }
    public string? StockName { get; set; }
}