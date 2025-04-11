namespace StockSimulator.Dtos;

public class UnassignedDividendDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime TradeDate { get; set; }
}