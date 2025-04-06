namespace StockSimulator.Dtos;
public class StockTransactionDto
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public string StockName { get; set; } = string.Empty;
    public int AgentId { get; set; }
    public string AgentName { get; set; } = string.Empty;
    public int BuyerId { get; set; }
    public string BuyerName { get; set; } = string.Empty;
    public DateTime TradeDate { get; set; }
    public DateTime SettlementDate { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
    public decimal? ConversionRate { get; set; }
    public decimal AgentFees { get; set; }
    public decimal StampDuty { get; set; }
    public bool IsSold { get; set; }

    // Calculated Fields
    public decimal Cost => UnitPrice * Quantity * (ConversionRate ?? 1) / 100;

    public decimal TotalAmount => IsSold
        ? (Cost * -1) - AgentFees - StampDuty
        : (Cost - AgentFees - StampDuty);
}