using StockSimulator.Data.Models;

namespace StockSimulator.Dtos.ProfitAndLoss;

public class StockProfitAndLossDataDto
{
    public int StockId { get; set; }
    public required string StockName { get; set; }
    public decimal QuantityHeld { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal GrossProfit { get; set; }
    public decimal TotalDividends { get; set; }
    public decimal TotalFees { get; set; }

    public List<TradeTransactionDto>? BuyTransactions { get; set; }
    public List<TradeTransactionDto>? SellTransactions { get; set; }
    public List<DividendDto>? Dividends { get; set; }
    public List<TradeFeeDto>? Fees { get; set; }
}