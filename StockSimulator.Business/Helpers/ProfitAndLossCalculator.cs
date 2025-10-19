using StockSimulator.Business.Dtos;
using StockSimulator.Data.Models;

namespace StockSimulator.Business.Logic;
public static class ProfitAndLossCalculator
{
    public static ProfitAndLossCalculationResult Calculate(IEnumerable<TradeTransaction> trades, IEnumerable<Dividend> dividends, IEnumerable<TradeFee> fees)
    {
        var agentId = trades.Where(t => t.IsSold).Select(a => a.AgentId).FirstOrDefault();
        var totalBuy = trades.Where(t => !t.IsSold).Sum(t => t.TransactionAmount);
        var totalSell = trades.Where(t => t.IsSold).Sum(t => t.TransactionAmount);
        var totalFee = fees.Sum(f => f.Amount);
        var totalDividends = dividends.Sum(d => d.Amount);
        var minDate = trades.Min(t => t.TradeDate);
        var maxDate = trades.Max(t => t.TradeDate);

        return new ProfitAndLossCalculationResult
        {
            GrossProfit = totalSell - totalBuy,
            TotalFees = totalFee,
            TotalDividends = totalDividends,
            DaysHolding = (maxDate - minDate).Days
        };
    }
}
