using TradeMatchingConsoleApp.Models;

namespace TradeMatchingConsoleApp.Data;

public static class TradeData
{
    public static List<TradeTransaction> GetSampleTrades()
    {
        return new List<TradeTransaction>
        {
            new() { Id = 1, StockId = 1, IsSold = false, Quantity = 10, TransactionAmount = 100, TradeDate = new DateTime(2024, 01, 01) },
            new() { Id = 2, StockId = 1, IsSold = true, Quantity = 10, TransactionAmount = 120, TradeDate = new DateTime(2024, 02, 01) },
            new() { Id = 3, StockId = 2, IsSold = false, Quantity = 5, TransactionAmount = 50, TradeDate = new DateTime(2024, 01, 01) },
            new() { Id = 4, StockId = 2, IsSold = true, Quantity = 5, TransactionAmount = 60, TradeDate = new DateTime(2024, 02, 01) },
            new() { Id = 5, StockId = 2, IsSold = false, Quantity = 5, TransactionAmount = 55, TradeDate = new DateTime(2024, 03, 01) }
        };
    }
}
