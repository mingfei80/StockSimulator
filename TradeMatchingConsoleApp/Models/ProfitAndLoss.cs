namespace TradeMatchingConsoleApp.Models;

public class ProfitAndLoss
{
    public int Id { get; set; }
    //public decimal ProfitOrLoss { get; set; }
    //public int DaysHolding { get; set; }
    public List<int> TradeIds { get; set; } = new();
}
