using Microsoft.EntityFrameworkCore;
using StockSimulator.Data.Context;
using StockSimulator.Data.Models.Projection;

namespace StockSimulator.Data.Repositories;

public class StockAnalyticsRepository : IStockAnalyticsRepository
{
    protected readonly StockSimulatorDbContext _context;
    public StockAnalyticsRepository(StockSimulatorDbContext context)
    {
        _context = context;
    }

    public async Task<List<StockProfitAndLossData>> GetStockProfitAndLossAsync()
    {
        var transactions = await _context.TradeTransactions
            .Include(t => t.Stock)
            .Where(t => t.ProfitAndLossId == null && t.Stock.StockCode.Substring(0,1) != "X")
            .ToListAsync();

        var dividends = await _context.Dividends.ToListAsync();
        var fees = await _context.TradeFees.ToListAsync();
        var prices = await _context.SnapshotStockPrices.ToDictionaryAsync(p => p.StockId, p => p.LocalUnitCost);

        var grouped = transactions
            .GroupBy(t => t.StockId)
            .Select(g =>
            {
                var stock = g.First().Stock;
                var buys = g.Where(t => !t.IsSold).ToList();
                var sells = g.Where(t => t.IsSold).ToList();
                var quantityHeld = buys.Sum(t => t.Quantity) - sells.Sum(t => t.Quantity);
                var currentPrice = prices.TryGetValue(g.Key, out var price) ? price/100 : 0; //cents to $ or pence to £
                var grossProfit = (quantityHeld * currentPrice) - buys.Sum(t => t.TransactionAmount) + sells.Sum(t => t.TransactionAmount);

                var stockDividends = dividends.Where(d => d.StockId == g.Key).ToList();
                var stockFees = fees.Where(f => f.StockId == g.Key).ToList();

                return new StockProfitAndLossData
                {
                    StockId = g.Key,
                    StockName = stock.StockName,
                    QuantityHeld = quantityHeld,
                    CurrentPrice = currentPrice,
                    GrossProfit = grossProfit,
                    TotalDividends = stockDividends.Sum(d => d.Amount),
                    TotalFees = stockFees.Sum(f => f.Amount),
                    BuyTransactions = buys,
                    SellTransactions = sells,
                    Dividends = stockDividends,
                    Fees = stockFees
                };
            })
            .ToList();

        return grouped;
    }
}
