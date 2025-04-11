namespace StockSimulator.Business.Dtos;

public class ProfitAndLossCalculationResult
{
    public int DaysHolding { get; set; }
    public decimal GrossProfit { get; set; }
    public decimal TotalFees { get; set; }
    public decimal TotalDividends { get; set; }
    public decimal NetProfit { 
        get { return GrossProfit - TotalFees + TotalDividends; }
    }
}
