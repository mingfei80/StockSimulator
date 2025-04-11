namespace TradeMatchingConsoleApp.Models;

public class TradeTransaction
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public bool IsSold { get; set; }
    public int Quantity { get; set; }
    public decimal TransactionAmount { get; set; }
    public DateTime TradeDate { get; set; }
    public int? ProfitAndLossId { get; set; }
}
